using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using INV.Data;
using INV.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace INV.Controllers
{
    public class InvestmentRoundsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvestmentRoundsController(ApplicationDbContext context)
        {
            _context = context;    
        }



        // GET: InvestmentRounds
        public async Task<IActionResult> Index()
        {
            var investmentRounds = _context.InvestmentRound
                .Include(ir => ir.Invention)
                .Include(ir => ir.ExpertService)
                .AsNoTracking();
            return View(await investmentRounds.ToListAsync());
        }

        // GET: InvestmentRounds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var investmentRound = await 
                _context.InvestmentRound
                .Include(m=>m.Invention)
                .ThenInclude(i=>i.ApplicationUser)
                .Include(m=>m.ExpertService)
                .Include(m=>m.Investments)
                .ThenInclude(ir=>ir.ApplicationUser)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (investmentRound == null)
            {
                return NotFound();
            }
            ViewData["isEditable"] = "false";
            if (investmentRound.Invention.ApplicationUserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                ViewData["isEditable"] = "true";
            }

            return View(investmentRound);
        }

        // GET: InvestmentRounds/Create
        public IActionResult Create()
        {
            PopulateInventionDropDownList();
            PopulateExpertServiceDropDownList();
            return View();
        }

        // POST: InvestmentRounds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DateCreated,Title,InventionID,ExpertServiceID, IsOpenForFunding")] InvestmentRound investmentRound)
        {
            if (ModelState.IsValid)
            {
                _context.Add(investmentRound);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            PopulateInventionDropDownList(investmentRound.Invention);
            PopulateExpertServiceDropDownList(investmentRound.ExpertService); 
            return View(investmentRound);
        }

        // GET: InvestmentRounds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var investmentRound = await _context.InvestmentRound.SingleOrDefaultAsync(m => m.ID == id);
            if (investmentRound == null)
            {
                return NotFound();
            }
            PopulateInventionDropDownList(investmentRound.Invention);
            PopulateExpertServiceDropDownList(investmentRound.ExpertService); 
            return View(investmentRound);
        }

        // POST: InvestmentRounds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DateCreated,Title,RaiseAmount,IsOpenForFunding")] InvestmentRound investmentRound)
        {
            if (id != investmentRound.ID)
            {
                return NotFound();
            }
            var investmentRoundOriginal = _context.InvestmentRound.Single(m => m.ID == id);

            if (ModelState.IsValid)
            {
                try
                {
                    //we just want to update updateable fields
                    investmentRoundOriginal.Title = investmentRound.Title;
                    investmentRoundOriginal.RaiseAmount = investmentRound.RaiseAmount;
                    investmentRoundOriginal.IsOpenForFunding = investmentRound.IsOpenForFunding;



                    _context.Update(investmentRoundOriginal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvestmentRoundExists(investmentRound.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Inventions", new { ID = investmentRoundOriginal.InventionID });
            }
            PopulateInventionDropDownList(investmentRound.Invention);
            return View(investmentRound);
        }

        private void PopulateInventionDropDownList(object selectedInvention = null)
        {
            ViewBag.InventionID = new SelectList(_context.Invention.OrderBy(i => i.Title).AsNoTracking(), "ID", "Title", selectedInvention);            
        }

        private void PopulateExpertServiceDropDownList(object selectedExpertService = null)
        {
            ViewBag.ExpertServiceID = new SelectList(_context.ExpertService.OrderBy(i => i.Title).AsNoTracking(), "ID", "Title", selectedExpertService);
        }

        // GET: InvestmentRounds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var investmentRound = await _context.InvestmentRound.SingleOrDefaultAsync(m => m.ID == id);
            if (investmentRound == null)
            {
                return NotFound();
            }

            return View(investmentRound);
        }

        // POST: InvestmentRounds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var investmentRound = await _context.InvestmentRound.SingleOrDefaultAsync(m => m.ID == id);
            _context.InvestmentRound.Remove(investmentRound);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<ActionResult> Invest (int id, [FromQuery]int investmentAmount)
        {
            //we need to create an investment that is associated with this investment round and with the active user

            var investmentRound = await _context.InvestmentRound.SingleOrDefaultAsync(m => m.ID == id);

            if (investmentRound == null)
            {
                return NotFound();
            }

            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            Investment investment = new Investment();
            //Invention invention = new Invention();
            investment.ApplicationUserId = user;
            investment.InvestmentRound = investmentRound;
            investment.InvestmentAmount = investmentAmount; 
            _context.Add(investment);
            _context.SaveChanges();
            TempData["invested"] = "true";
            //TempData["message"] = "Awesome! Your request to invest is submitted, and you will be notified by the inventor if the round closes and you are part of it. ";
            return RedirectToAction("Details", "Inventions", new { ID = investmentRound.InventionID});

        }

        private bool InvestmentRoundExists(int id)
        {
            return _context.InvestmentRound.Any(e => e.ID == id);
        }
    }
}

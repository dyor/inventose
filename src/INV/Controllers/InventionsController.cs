using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using INV.Data;
using INV.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using INV.Models.InventionViewModels;

namespace INV.Controllers
{
    public class InventionsController : Controller
    {
        IAuthorizationService _authorizationService;



        //bigtime dependency injection
        //private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;

        public InventionsController(ApplicationDbContext context, IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        // GET: Inventions
        public async Task<IActionResult> Index()
        {
            var mex = Environment.CurrentManagedThreadId; 
            ViewData["Title"] = "All Inventions";
            var invention = _context.Invention
                .Include(i => i.ApplicationUser)
                .Where(i => i.IsPublished)
                .OrderByDescending(i => i.DateCreated);

            return View("Index", await invention.ToListAsync());
        }

        // GET: Invest
        public async Task<IActionResult> Invest()
        {
            var invention = _context.Invention
                .Include(i=>i.ApplicationUser)
                .Where(i => i.IsOpenForFunding == true)
                //.Where(i=>i.AnotherIsOpenForFunding() == true)
                .Where(i => i.IsPublished)
                .OrderByDescending(i => i.DateCreated); 


            ViewData["Title"] = "Invest in Inventions";

            return View("Index", await invention.ToListAsync());
        }

        // GET: Buy
        public async Task<IActionResult> Buy()
        {
            var invention = _context.Invention
                .Include(i => i.ApplicationUser)
                .Where(i => i.IsForSale == true)
                .Where(i => i.IsPublished)
                .OrderByDescending(i => i.DateCreated)
                .AsNoTracking();
            ViewData["Title"] = "Buy Inventions";
            return View("Index", await invention.ToListAsync());
        }

        // GET: Inventions/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var invention = await _context.Invention
                .Include(i => i.InvestmentRounds) 
                .ThenInclude(ir=>ir.RoundStatus)
                .Include(i => i.InvestmentRounds)
                .ThenInclude(ir => ir.Investments)
                .ThenInclude(iv=>iv.ApplicationUser)
                .Include(i => i.InvestmentRounds)
                .ThenInclude(ir => ir.ExpertService)
                //this is the application user for the investment
                .ThenInclude(i=>i.ApplicationUser)
                .Include(i=>i.ApplicationUser)
                .SingleOrDefaultAsync(m => m.ID == id);

            if (invention == null)
            {
                return NotFound();
            }
            ViewData["isEditable"] = "false";
            if (invention.ApplicationUserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                ViewData["isEditable"] = "true"; 
            }
            return View(invention); 
        }

        [Authorize]
        // GET: Inventions/Create
        public async Task<IActionResult> Create()
        {
            //we are seeing if the user has an invention already
            var invention = await _context.Invention
                .Include(i => i.ApplicationUser)
                .ThenInclude(au => au.Inventions)
                .Where(i=>i.ApplicationUserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .FirstOrDefaultAsync();
            if (invention!=null && invention.ApplicationUser!=null && invention.ApplicationUser.Inventions != null)
            {
                TempData["hasInventions"] = "true"; 
            }
            return View();
        }

        // POST: Inventions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DateCreated,IsForSale,Title,IsOpenForFunding,LongDescription")] Invention invention)
        {
            

            if (ModelState.IsValid)
            {
                var user = User.FindFirst(ClaimTypes.NameIdentifier).Value; 
                invention.ApplicationUserId = user; 
                _context.Add(invention);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { ID = invention.ID });
            }
            return View(invention);
        }

        [Authorize]
        // GET: Inventions/CreateForSale
        public IActionResult CreateForSale()
        {
            TempData["ForSale"] = "true";
            return View("Create");
        }


        [Authorize]
        // GET: Inventions/CreateDIY
        public IActionResult CreateDIY()
        {
            return View();
        }

        // POST: Inventions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDIY([Bind("Invention.Title,Invention.LongDescription,Invention.Valuation,ExpertService.Title,ExpertService.Cost,ExpertService.MaxDaysToComplete")] InventionViewModel inventionCollection)
        {
            //two ways to chain: 1) add a couple or triple of models to a view controller, or 2) just chain them together with adds. Think #2 is better. 

            if (ModelState.IsValid)
            {

                var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Invention invention = new Invention();
                invention.Title = Request.Form["Invention.Title"];
                invention.LongDescription = Request.Form["Invention.LongDescription"];
                invention.Valuation = Convert.ToInt16(Request.Form["Invention.Valuation"]);
                invention.IsForSale = false;
                invention.IsOpenForFunding = true;
                invention.IsPublished = true;
                invention.ApplicationUserId = user; 
                inventionCollection.Invention = invention; 
                inventionCollection.Invention.ApplicationUserId = user;
                ExpertService expertService = new ExpertService();
                expertService.Title = Request.Form["ExpertService.Title"];
                expertService.IsPublished = false;
                expertService.LongDescription = "I will use the funding to work on my invention and to pay for expenses.";
                expertService.MaxDaysToComplete = Convert.ToInt16(Request.Form["ExpertService.MaxDaysToComplete"]);
                expertService.Cost = Convert.ToInt16(Request.Form["ExpertService.Cost"]);
                expertService.ServiceTypeID = 2;
                expertService.ApplicationUserId = user; 
                inventionCollection.ExpertService = expertService; 
                _context.Add(invention);
                _context.Add(expertService);
                _context.SaveChanges();
                InvestmentRound investmentRound = new InvestmentRound();
                investmentRound.InventionID = invention.ID;
                investmentRound.RaiseAmount = expertService.Cost;
                investmentRound.Title = "DIY Round";
                investmentRound.IsOpenForFunding = true;
                investmentRound.ExpertServiceID = expertService.ID;
                investmentRound.RoundStatusID = 1; 
                _context.Add(investmentRound);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { ID = inventionCollection.Invention.ID });
            }
            return View(inventionCollection);
        }


        // GET: Inventions/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invention = await _context
                .Invention
                .Include(m=>m.InvestmentRounds)
                .ThenInclude(ir=>ir.ExpertService)
                .ThenInclude(es=>es.ServiceType)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (invention == null)
            {
                return NotFound();
            }

            if (await _authorizationService.AuthorizeAsync(User, invention, Operations.Update))
            {
                ViewData["Title"] = "Describe " + invention.Title;
                if (TempData["isNew"] != null)
                {
                    invention.Title = "";
                }
                //lets see if there is an open investment round to set IsOpenForFunding
                if (invention.InvestmentRounds != null && invention.InvestmentRounds.Where(ir => ir.IsOpenForFunding).Count() > 0)
                {
                    invention.IsOpenForFunding = true;
                }
                else if ((invention.InvestmentRounds == null) || (invention.InvestmentRounds.Where(ir => ir.IsOpenForFunding).Count() == 0)) 
                {
                    invention.IsOpenForFunding = false;
                }

                
                //
                return View(invention);
            }
            else
            {
                return new ChallengeResult();
            }
        }

        // POST: Inventions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string published, [Bind("ID,DateCreated,IsForSale,Title,IsOpenForFunding,LongDescription,IsPublished,Valuation,InvestmentRounds")] Invention invention)
        {

            if (id != invention.ID)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    if (published=="Save and Publish")
                    {
                        invention.IsPublished = true; 
                    }
                    else
                    {
                        invention.IsPublished = false; 
                    }
                    invention.ApplicationUserId = user;
                    //should be delted jan 30 - being handled in the details stage. 
                    //if (invention.InvestmentRounds != null && invention.InvestmentRounds.Where(ir => ir.IsOpenForFunding).Count() > 0)
                    //{
                    //    invention.IsOpenForFunding = true; 
                    //}
                    //else
                    //{
                    //    invention.IsOpenForFunding = false; 
                    //}

                    _context.Update(invention);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventionExists(invention.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new { ID = id });

            }
            return View(invention);
        }

        // GET: Inventions/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invention = await _context.Invention.SingleOrDefaultAsync(m => m.ID == id);
            if (invention == null)
            {
                return NotFound();
            }

            return View(invention);
        }

        // POST: Inventions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invention = await _context.Invention.SingleOrDefaultAsync(m => m.ID == id);
            _context.Invention.Remove(invention);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool InventionExists(int id)
        {
            return _context.Invention.Any(e => e.ID == id);
        }
    }
    public static class Operations
    {
        public static OperationAuthorizationRequirement Create =
            new OperationAuthorizationRequirement { Name = "Create" };
        public static OperationAuthorizationRequirement Read =
            new OperationAuthorizationRequirement { Name = "Read" };
        public static OperationAuthorizationRequirement Update =
            new OperationAuthorizationRequirement { Name = "Update" };
        public static OperationAuthorizationRequirement Delete =
            new OperationAuthorizationRequirement { Name = "Delete" };
    }
    public class InventionAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Invention>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                    OperationAuthorizationRequirement requirement,
                                                    Invention resource)
        {
            //handle updates
            if (requirement.Name == "Update" || requirement.Name == "Delete")
            {
                if (resource.ApplicationUserId == context.User.FindFirstValue(ClaimTypes.NameIdentifier))
                {
                    context.Succeed(requirement);
                }
                //if they are an admin, also let them edit. 
                if (context.User.IsInRole("Admin"))
                {
                    context.Succeed(requirement);
                }
                
            }
            return Task.CompletedTask;
        }
    }
    

}

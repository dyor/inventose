using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using INV.Data;
using INV.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;

namespace INV.Controllers
{
    public class ExpertServicesController : Controller
    {
        IAuthorizationService _authorizationService;
        private readonly ApplicationDbContext _context;

        private void PopulateServiceTypeDropDownList(object selectedServiceType = null)
        {
            ViewBag.ServiceTypeID = new SelectList(_context.ServiceType.OrderBy(i => i.Title).AsNoTracking(), "ID", "Title", selectedServiceType);

        }
 
        public ExpertServicesController(ApplicationDbContext context, IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        // GET: ExpertServices
        public async Task<IActionResult> Index()
        {
            var expertServices = _context.ExpertService
                .Include(es => es.ServiceType)
                .Include(es => es.ApplicationUser)
                .AsNoTracking();
            ViewData["Title"] = "Browse All Experts";

            return View(await expertServices.ToListAsync());
        }

        // GET: PatentServices
        public async Task<IActionResult> Patent()
        {
            var expertServices = _context.ExpertService
                .Include(es => es.ServiceType)
                .Include(es=>es.ApplicationUser)
                .Where(es => es.ServiceType.Title == "Patent")
                .AsNoTracking();
            ViewData["Title"] = "Find a Patent Attorney";

            return View("Index", await expertServices.ToListAsync());
        }

        // GET: PrototypeServices
        public async Task<IActionResult> Prototype()
        {
            var expertServices = _context.ExpertService
                .Include(es => es.ServiceType)
                .Include(es => es.ApplicationUser)
                .Where(es => es.ServiceType.Title == "Prototype")
                .AsNoTracking();
            ViewData["Title"] = "Find a Prototype Builder";

            return View("Index", await expertServices.ToListAsync());
        }
        [Authorize]
        // GET: ExpertServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expertService = await _context.ExpertService.Include(m => m.ServiceType).Include(m=>m.ApplicationUser).SingleOrDefaultAsync(m => m.ID == id);
            if (expertService == null)
            {
                return NotFound();
            }
            ViewData["isEditable"] = "false";
            if (expertService.ApplicationUserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                ViewData["isEditable"] = "true";
            }
            return View(expertService);
        }

        // GET: ExpertServices/Create
        [Authorize]
        public IActionResult Create()
        {
            PopulateServiceTypeDropDownList();
            return View();
        }

        public IActionResult CreatePatent()
        {
            PopulateServiceTypeDropDownList(1);
            return View("Create");
        }
        public IActionResult CreatePrototype()
        {
            PopulateServiceTypeDropDownList(2);
            return View("Create");
        }
        public IActionResult CreateOther()
        {
            PopulateServiceTypeDropDownList(3);
            return View("Create");
        }

        // POST: ExpertServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("ID,Cost,MaxDaysToComplete,ServiceTypeID,Title,LongDescription,IsPublished")] ExpertService expertService)
        {
            if (ModelState.IsValid)
            {
                var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                expertService.ApplicationUserId = user;
                _context.Add(expertService);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            PopulateServiceTypeDropDownList(expertService.ServiceType);
            return View(expertService);
        }

        // GET: ExpertServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expertService = await _context.ExpertService.SingleOrDefaultAsync(m => m.ID == id);
            if (expertService == null)
            {
                return NotFound();
            }
            PopulateServiceTypeDropDownList(expertService.ServiceType);

            if (await _authorizationService.AuthorizeAsync(User, expertService, Operations.Update))
            {
                return View(expertService);
            }
            else
            {
                return new ChallengeResult();
            }
            
        }

        // POST: ExpertServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Cost,MaxDaysToComplete,ServiceTypeID,Title,LongDescription,IsPublished,IsOpenForFunding")] ExpertService expertService)
        {
            if (id != expertService.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    expertService.ApplicationUserId = user;
                    _context.Update(expertService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpertServiceExists(expertService.ID))
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
            PopulateServiceTypeDropDownList(expertService.ServiceType);
            return View(expertService);
        }

        // GET: ExpertServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expertService = await _context.ExpertService.SingleOrDefaultAsync(m => m.ID == id);
            if (expertService == null)
            {
                return NotFound();
            }

            return View(expertService);
        }

        // POST: ExpertServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expertService = await _context.ExpertService.SingleOrDefaultAsync(m => m.ID == id);
            _context.ExpertService.Remove(expertService);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ExpertServiceExists(int id)
        {
            return _context.ExpertService.Any(e => e.ID == id);
        }

        public IActionResult Purchase()
        {

            return View();

        }
        [Authorize]
        public async Task<IActionResult> Invest(int? id)
        {
            //if user is not logged in, direct them to login or create account (with a redirect back to here) - should be automatic



            //if the user does not have an invention, direct them to create one (with a redirect back to here)
            //first we need to associate inventions with the active user. 
            //to start, lets just create an invention

            if (id == null)
            {
                return NotFound();
            }

            var expertService = await _context.ExpertService.Include(m => m.ServiceType).SingleOrDefaultAsync(m => m.ID == id);

            if (expertService == null)
            {
                return NotFound();
            }

            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            Invention invention = new Invention(); 

            invention.ApplicationUserId = user;
            invention.Title = User.Identity.Name +"'s Invention";
            invention.IsOpenForFunding = true;
            //this will result in 20% of the invention being allocated to the round
            invention.Valuation = expertService.Cost * 4;
            invention.IsPublished = true; 
            
            _context.Add(invention);
            _context.SaveChanges();

            //await _context.SaveChangesAsync();


            //if the user has multiple inventions, ask them to select which one, assume the last updated and give them an option to change

            //now add an investment round, with the expert service and the invention specified - create investment round and associate with invention and expert service

            InvestmentRound investmentRound = new InvestmentRound();
            investmentRound.ExpertServiceID = expertService.ID;
            investmentRound.InventionID = invention.ID;
            investmentRound.RaiseAmount = expertService.Cost;
            investmentRound.Title = string.Format("Round for {0}", expertService.Title);
            investmentRound.IsOpenForFunding = true;
            investmentRound.RoundStatusID = 1; 
            

            _context.Add(investmentRound);
            await _context.SaveChangesAsync();
            //TempData["message"] = "Update Invention Title and add Long Description to.  that tells people just enough to get them to invest but without disclosing patentable subject matter, then click on Save and Publish to get it listed under the Invest tab!";
            TempData["isNew"] = "true";
            return RedirectToAction("Edit", "Inventions", new { ID = invention.ID });

            //We will have the amount to raise, we need to ask what the Current Value of the invention is and minimum investment - now this action should be done, and we should transition to investment round

            //Then we can calculate the equity that all investors are getting and the amount of equity for the minimum investment

            //Then this one should be marked open for funding



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
        public class ExpertServiceAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, ExpertService>
        {
            protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                        OperationAuthorizationRequirement requirement,
                                                        ExpertService resource)
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
}

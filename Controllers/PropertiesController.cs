using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateManagement.Data;
using RealEstateManagement.Models;

namespace RealEstateManagement.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PropertiesController> _logger;

        public PropertiesController(ApplicationDbContext context, ILogger<PropertiesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Properties
        public async Task<IActionResult> Index()
        {
            var properties = _context.Properties.Include(p => p.Agent);
            return View(await properties.ToListAsync());
        }

        // GET: Properties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var property = await _context.Properties
                .Include(p => p.Agent)
                .Include(p => p.Contracts)
                .FirstOrDefaultAsync(m => m.Property_ID == id);
            
            if (property == null)
            {
                return NotFound();
            }

            return View(property);
        }

        // GET: Properties/Create
        public IActionResult Create()
        {
            ViewData["AgentID"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Agents, "Agent_ID", "Name");
            return View();
        }

        // POST: Properties/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Address,PropertyType,Area,Price,Status,Agent_ID")] Property property)
        {
            if (ModelState.IsValid)
            {
                _context.Add(property);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Log ModelState errors and form values for debugging
            _logger.LogWarning("ModelState is invalid when creating Property.");
            foreach (var kv in Request.Form.Keys)
            {
                _logger.LogWarning("Form field: {Key} = {Value}", kv, Request.Form[kv]);
            }
            foreach (var ms in ViewData.ModelState)
            {
                foreach (var err in ms.Value.Errors)
                {
                    _logger.LogWarning("ModelState error for {Key}: {Error}", ms.Key, err.ErrorMessage);
                }
            }

            ViewData["AgentID"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Agents, "Agent_ID", "Name", property.Agent_ID);
            return View(property);
        }

        // GET: Properties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var property = await _context.Properties.FindAsync(id);
            if (property == null)
            {
                return NotFound();
            }
            ViewData["AgentID"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Agents, "Agent_ID", "Name", property.Agent_ID);
            return View(property);
        }

        // POST: Properties/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Property_ID,Address,PropertyType,Area,Price,Status,Agent_ID")] Property property)
        {
            if (id != property.Property_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(property);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyExists(property.Property_ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AgentID"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Agents, "Agent_ID", "Name", property.Agent_ID);
            return View(property);
        }

        // GET: Properties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var property = await _context.Properties
                .Include(p => p.Agent)
                .FirstOrDefaultAsync(m => m.Property_ID == id);
            if (property == null)
            {
                return NotFound();
            }

            return View(property);
        }

        // POST: Properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property != null)
            {
                _context.Properties.Remove(property);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyExists(int id)
        {
            return _context.Properties.Any(e => e.Property_ID == id);
        }
    }
}
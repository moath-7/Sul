using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateManagement.Data;
using RealEstateManagement.Models;

namespace RealEstateManagement.Controllers
{
    public class ContractsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContractsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Contracts
        public async Task<IActionResult> Index()
        {
            var contracts = _context.Contracts
                .Include(c => c.Customer)
                .Include(c => c.Property)
                .Include(c => c.Agent);
            return View(await contracts.ToListAsync());
        }

        // GET: Contracts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts
                .Include(c => c.Customer)
                .Include(c => c.Property)
                .Include(c => c.Agent)
                .Include(c => c.Payments)
                .FirstOrDefaultAsync(m => m.Contract_ID == id);
            
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // GET: Contracts/Create
        public IActionResult Create()
        {
            ViewData["CustomerID"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Customers, "Customer_ID", "Name");
            ViewData["PropertyID"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Properties, "Property_ID", "Address");
            ViewData["AgentID"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Agents, "Agent_ID", "Name");
            return View();
        }

        // POST: Contracts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Property_ID,Customer_ID,Agent_ID,ContractType,ContractDate,Amount")] Contract contract)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contract);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Customers, "Customer_ID", "Name", contract.Customer_ID);
            ViewData["PropertyID"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Properties, "Property_ID", "Address", contract.Property_ID);
            ViewData["AgentID"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Agents, "Agent_ID", "Name", contract.Agent_ID);
            return View(contract);
        }

        // GET: Contracts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Customers, "Customer_ID", "Name", contract.Customer_ID);
            ViewData["PropertyID"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Properties, "Property_ID", "Address", contract.Property_ID);
            ViewData["AgentID"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Agents, "Agent_ID", "Name", contract.Agent_ID);
            return View(contract);
        }

        // POST: Contracts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Contract_ID,Property_ID,Customer_ID,Agent_ID,ContractType,ContractDate,Amount")] Contract contract)
        {
            if (id != contract.Contract_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contract);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractExists(contract.Contract_ID))
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
            ViewData["CustomerID"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Customers, "Customer_ID", "Name", contract.Customer_ID);
            ViewData["PropertyID"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Properties, "Property_ID", "Address", contract.Property_ID);
            ViewData["AgentID"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Agents, "Agent_ID", "Name", contract.Agent_ID);
            return View(contract);
        }

        // GET: Contracts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts
                .Include(c => c.Customer)
                .Include(c => c.Property)
                .Include(c => c.Agent)
                .FirstOrDefaultAsync(m => m.Contract_ID == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            if (contract != null)
            {
                _context.Contracts.Remove(contract);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ContractExists(int id)
        {
            return _context.Contracts.Any(e => e.Contract_ID == id);
        }
    }
}
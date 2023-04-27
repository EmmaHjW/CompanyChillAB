using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CompanyChillAB.Data;
using CompanyChillAB.Models;

namespace CompanyChillAB.Controllers
{
    public class LeaveListsController : Controller
    {
        private readonly CompanyChillABContext _context;

        public LeaveListsController(CompanyChillABContext context)
        {
            _context = context;
        }

        // GET: LeaveLists
        //public async Task<IActionResult> Index()
        //{
        //    var companyChillABContext = _context.LeaveLists.Include(l => l.Employees);
        //    return View(await companyChillABContext.ToListAsync());
        //}

        public async Task<IActionResult> Index()
        {
            var leaveLists = await _context.LeaveLists
                .Include(ll => ll.Employees)
                .ToListAsync();

            return View(leaveLists);
        }


        // GET: LeaveLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LeaveLists == null)
            {
                return NotFound();
            }

            var leaveList = await _context.LeaveLists
                .Include(l => l.Employees)
                .FirstOrDefaultAsync(m => m.LeaveListId == id);
            if (leaveList == null)
            {
                return NotFound();
            }

            return View(leaveList);
        }

        // GET: LeaveLists/Create
        public IActionResult Create()
        {
            ViewData["FK_EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name");
            return View();
        }

        // POST: LeaveLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeaveListId,Start,Stop,LeaveType,Description,FK_EmployeeId,FK_LeaveId")] LeaveList leaveList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leaveList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FK_EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name", leaveList.FK_EmployeeId);
            return View(leaveList);
        }

        // GET: LeaveLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LeaveLists == null)
            {
                return NotFound();
            }

            var leaveList = await _context.LeaveLists.FindAsync(id);
            if (leaveList == null)
            {
                return NotFound();
            }
            ViewData["FK_EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name", leaveList.FK_EmployeeId);
            return View(leaveList);
        }

        // POST: LeaveLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeaveListId,Start,Stop,LeaveType,Description,FK_EmployeeId,FK_LeaveId")] LeaveList leaveList)
        {
            if (id != leaveList.LeaveListId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveListExists(leaveList.LeaveListId))
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
            ViewData["FK_EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name", leaveList.FK_EmployeeId);
            return View(leaveList);
        }

        // GET: LeaveLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LeaveLists == null)
            {
                return NotFound();
            }

            var leaveList = await _context.LeaveLists
                .Include(l => l.Employees)
                .FirstOrDefaultAsync(m => m.LeaveListId == id);
            if (leaveList == null)
            {
                return NotFound();
            }

            return View(leaveList);
        }

        // POST: LeaveLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LeaveLists == null)
            {
                return Problem("Entity set 'CompanyChillABContext.LeaveLists'  is null.");
            }
            var leaveList = await _context.LeaveLists.FindAsync(id);
            if (leaveList != null)
            {
                _context.LeaveLists.Remove(leaveList);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveListExists(int id)
        {
          return (_context.LeaveLists?.Any(e => e.LeaveListId == id)).GetValueOrDefault();
        }
    }
}

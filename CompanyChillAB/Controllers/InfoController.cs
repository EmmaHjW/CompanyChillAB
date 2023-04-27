using CompanyChillAB.Data;
using CompanyChillAB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyChillAB.Controllers
{
	public class InfoController : Controller
	{
		private readonly CompanyChillABContext context;
        public InfoController(CompanyChillABContext _context)
        {
			context = _context;
        }
        public async Task<IActionResult> Index()
        {
            List<InfoViewModel> list = new List<InfoViewModel>();
            var items = await (from emp in context.Employees
                               join ll in context.LeaveLists on emp.EmployeeId equals ll.FK_EmployeeId
                               select new
                               {
                                   Start = ll.Start,
                                   Stop = ll.Stop,
                                   LeaveType = ll.LeaveType,
                                   Description = ll.Description,
                                   Name = emp.Name,
                                   Department = emp.Department
                               }).ToListAsync();
            // konvertera från anonymous
            foreach (var item in items)
            {
                InfoViewModel listItem = new InfoViewModel();
                listItem.Start = item.Start;
                listItem.Stop = item.Stop;
                listItem.LeaveType = item.LeaveType;
                listItem.Description = item.Description;
                listItem.Name = item.Name;
                listItem.Department = item.Department;
                list.Add(listItem);
            }
            return View(list);

            //return context.InfoViewModel != null ?
            //            View(await context.Employees.ToListAsync()) :
            //            Problem("Entity set 'CompanyChillABContext.InfoViewModel'  is null.");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeaveType,Description,Start,Stop,Name,Department")] InfoViewModel infoViewModel)
        {
            if (ModelState.IsValid)
            {
                var leaveList = new LeaveList
                {
                    LeaveType = infoViewModel.LeaveType,
                    Description = infoViewModel.Description,
                    Start = infoViewModel.Start,
                    Stop = infoViewModel.Stop
                };

               
                var employee = await context.Employees.FirstOrDefaultAsync(e => e.Name == infoViewModel.Name && e.Department == infoViewModel.Department);

                if (employee == null)
                {
                    ModelState.AddModelError(string.Empty, "Employee not found.");
                    return View(infoViewModel);
                }

                leaveList.Employees = employee;
                context.LeaveLists.Add(leaveList);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(infoViewModel);
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("LeaveType,Description,Start,Stop,Name,Department")] InfoViewModel infoViewModel)
        //{
        //    var MyList = infoViewModel;
        //    IList<InfoViewModel> MyList2 = IList<InfoViewModel>().ToList();
        //    foreach (var item in infoViewModel)
        //    {
        //        //InfoViewModel listItem = new InfoViewModel();
        //        LeaveList leavelist = new LeaveList();
        //        leavelist.Start = item.Start;
        //        leavelist.Stop = item.Stop;
        //        leavelist.LeaveType = item.LeaveType;
        //        leavelist.Description = item.Description;
        //        //leavelist.Name = item.Employees.Name;
        //        //leavelist.Department = item.Employees.Department;
        //        leavelist.Add(leavelist);
        //    }
        //    return View(list);
        //    if (ModelState.IsValid)
        //    {
        //        context.Add(infoViewModel);
        //        await context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(infoViewModel);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("LeaveType,Description,Start,Stop,Name,Department")] InfoViewModel infoViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Skapa en instans av Employee baserat på infoViewModel
        //        var employee = new Employee
        //        {
        //            Name = infoViewModel.Name,
        //            Department = infoViewModel.Department
        //        };

        //        // Skapa en lista för LeaveList-objekt
        //        var leaveLists = new List<LeaveList>();

        //        // Skapa en LeaveList-instans för varje objekt i infoViewModel
        //        foreach (var item in infoViewModel.LeaveList)
        //        {
        //            var leaveList = new LeaveList
        //            {
        //                Start = item.StartDate,
        //                Stop = item.StopDate,
        //                LeaveType = item.LeaveType,
        //                Description = item.Description,
        //                Employee = employee // Koppla LeaveList till Employee-objektet
        //            };

        //            leaveLists.Add(leaveList);
        //        }

        //        // Spara Employee- och LeaveList-objekten till databasen
        //        context.Add(employee);
        //        context.AddRange(leaveLists);
        //        await context.SaveChangesAsync();

        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View(infoViewModel);
        //}
    }
}

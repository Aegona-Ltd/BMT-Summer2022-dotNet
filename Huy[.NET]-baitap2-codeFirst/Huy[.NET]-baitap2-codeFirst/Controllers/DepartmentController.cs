using Huy_.NET__baitap2_codeFirst.Data;
using Huy_.NET__baitap2_codeFirst.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Huy_.NET__baitap2_codeFirst.Entities;
using Huy_.NET__baitap2_codeFirst.Models;

namespace Huy_.NET__baitap2_codeFirst.Controllers
{
    [Route("department")]
    public class DepartmentController : Controller
    {
        private DepartmentService departmentService;
        private SchoolContext db;
        public DepartmentController(DepartmentService _departmentService, SchoolContext _db)
        {
            departmentService = _departmentService;
            db = _db;
        }
        [Route("index")]
        public async Task<IActionResult> Index()
        {
             var departments = await departmentService.FindAll().ToListAsync();
            return View(departments);
        }
        [Route("details")]
        public async Task<IActionResult> Details(int? id)
        {
            var department = await departmentService.FindDpm(id.Value);
            return View(department);
        }
        [HttpGet]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            DepartmentInfo departmentInfo = await departmentService.FindDpm(id);
            ViewData["InstructorID"] = new SelectList(db.Instructors, "ID", "FullName", departmentInfo.InstructorID);
            return View("Edit", departmentInfo);
        }
        [HttpPost]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int? id, byte[] rowVersion)
        {
            if (id == null)
            {
                return NotFound();
            }
            var departmentToUpdate1 = await departmentService.FindDpm(id.Value);
            if(departmentToUpdate1 == null)
            {
                Department deleteDepartmentInfo = new Department();
                await TryUpdateModelAsync(deleteDepartmentInfo);
                ModelState.AddModelError(string.Empty,
                "Unable to save changes. The department was deleted by another user.");
                ViewData["InstructorID"] = new SelectList(db.Instructors, "ID", "FullName", deleteDepartmentInfo.InstructorID);
                return View("Edit", deleteDepartmentInfo);
            }
            Department departmentToUpdate = new Department()
            {
                DepartmentID = departmentToUpdate1.DepartmentID,
                Budget = departmentToUpdate1.Budget,
                Name = departmentToUpdate1.Name,
                StartDate = departmentToUpdate1.StartDate,
                InstructorID = departmentToUpdate1.InstructorID,
                Administrator = departmentToUpdate1.Administrator,
                RowVersion = departmentToUpdate1.RowVersion
            };
            db.Entry(departmentToUpdate).Property("RowVersion").OriginalValue = rowVersion;
            if (await TryUpdateModelAsync<Department>(
                departmentToUpdate,
                "",
                s => s.Name, s => s.StartDate, s => s.Budget, s => s.InstructorID))
            {
                try
                {
                    db.Entry(departmentToUpdate).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (Department)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty,
                            "Unable to save changes. The department was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Department)databaseEntry.ToObject();

                        if (databaseValues.Name != clientValues.Name)
                        {
                            ModelState.AddModelError("Name", $"Current value: {databaseValues.Name}");
                        }
                        if (databaseValues.Budget != clientValues.Budget)
                        {
                            ModelState.AddModelError("Budget", $"Current value: {databaseValues.Budget:c}");
                        }
                        if (databaseValues.StartDate != clientValues.StartDate)
                        {
                            ModelState.AddModelError("StartDate", $"Current value: {databaseValues.StartDate:d}");
                        }
                        if (databaseValues.InstructorID != clientValues.InstructorID)
                        {
                            Instructor databaseInstructor = await db.Instructors.FirstOrDefaultAsync(i => i.ID == databaseValues.InstructorID);
                            ModelState.AddModelError("InstructorID", $"Current value: {databaseInstructor?.FullName}");
                        }

                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                                + "was modified by another user after you got the original value. The "
                                + "edit operation was canceled and the current values in the database "
                                + "have been displayed. If you still want to edit this record, click "
                                + "the Save button again. Otherwise click the Back to List hyperlink.");
                        departmentToUpdate.RowVersion = (byte[])databaseValues.RowVersion;
                        ModelState.Remove("RowVersion");
                    }
                }
            }
            ViewData["InstructorID"] = new SelectList(db.Instructors, "ID", "FullName", departmentToUpdate1.InstructorID);
            return View("Edit", departmentToUpdate1);
        }
        [HttpGet]
        [Route("deleteinfo/{id}")]
        public async Task<IActionResult> DeleteInfo(int? id, bool? concurrencyError)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentInfo = await departmentService.FindDpm(id.Value);
            if (departmentInfo == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }

            if (concurrencyError.GetValueOrDefault())
            {
                ViewData["ConcurrencyErrorMessage"] = "The record you attempted to delete "
                    + "was modified by another user after you got the original values. "
                    + "The delete operation was canceled and the current values in the "
                    + "database have been displayed. If you still want to delete this "
                    + "record, click the Delete button again. Otherwise "
                    + "click the Back to List hyperlink.";
            }

            return View(departmentInfo);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Department department)
        {
            try
            {
                if (await db.Departments.AnyAsync(m => m.DepartmentID == department.DepartmentID))
                {
                    db.Departments.Remove(department);
                    await db.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(DeleteInfo), new { concurrencyError = true, id = department.DepartmentID });
            }
        }
        [HttpGet]
        [Route("create")]
        public async Task<IActionResult> Create()
        {
            DepartmentInfo departmentInfo = new DepartmentInfo();
            ViewData["InstructorID"] = new SelectList(db.Instructors, "ID", "FullName", departmentInfo.InstructorID);
            return View("Create", departmentInfo);
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(DepartmentInfo departmentInfo)
        {
            if (ModelState.IsValid)
            {
                departmentService.Create(departmentInfo);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InstructorID"] = new SelectList(db.Instructors, "ID", "FullName", departmentInfo.InstructorID);
            return View("Create", departmentInfo);
        }
    }
}

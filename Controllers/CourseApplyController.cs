using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EFCoreApp.Controllers
{
    public class CourseApplyController: Controller
    {
        private readonly DataContext _context;

        public CourseApplyController(DataContext context){
            _context = context;
        }

        public async Task<IActionResult> Index(){
            var courseApplications = await _context
                                    ._courseapplies
                                    .Include(x => x.Student)
                                    .Include(x => x.Course)
                                    .ToListAsync();
            return View(courseApplications);
        }

        public async Task<IActionResult> CreateCourseApply(){
            ViewBag.Students = new SelectList(await _context._students.ToListAsync(), "StudentId", "StudentFullName");
            ViewBag.Courses = new SelectList(await _context._courses.ToListAsync(), "CourseId", "CourseName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourseApply(CourseApply model){
            if(model == null){
                return NotFound();
            }

            if(ModelState.IsValid){
                model.CourseApplyDate = DateTime.Now;
                _context._courseapplies.Add(model);
                await _context.SaveChangesAsync();
                
                return RedirectToAction("Index");
            }

            ViewBag.Students = new SelectList(await _context._students.ToListAsync(), "StudentId", "StudentFullName");
            ViewBag.Courses = new SelectList(await _context._courses.ToListAsync(), "CourseId", "CourseName");
            return View(model);
        }
    }
}
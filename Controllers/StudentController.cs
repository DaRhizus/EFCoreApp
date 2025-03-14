using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace EFCoreApp.Controllers
{
    public class StudentController: Controller
    {
        private readonly DataContext _context;

        public StudentController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(){
            var students = await _context._students.ToListAsync();
            return View(students);
        }

        public IActionResult CreateStudent(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(Student student){
            if(ModelState.IsValid){
                _context._students.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else{
                return View(student);
            }
        }


        public async Task<IActionResult> StudentDetails(int? id){
            if(id == null){
                return NotFound(id);
            }
            
            var student = await _context
                                ._students
                                .Include(x=>x.CourseApplies)
                                .ThenInclude(x=>x.Course)
                                .FirstOrDefaultAsync(x=>x.StudentId == id);

            if(student == null){
                return NotFound();
            }

            return View(student);
        }

        public async Task<IActionResult> EditStudent(int? id){
            if (id == null){
                return NotFound();
            }
            
            var student = await _context._students.FindAsync(id);

            if(student == null){
                return NotFound();
            }

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStudent(int? id, Student student){
            if (id == null){
                return NotFound();
            }

            if(ModelState.IsValid){
                try{
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch(DbUpdateConcurrencyException){
                    if(!_context._students.Any(x => x.StudentId == student.StudentId)){
                        return NotFound();
                    }
                    else{
                        throw;
                    }
                }
            }
            else{
                return View(student);
            }
        }
    
        public async Task<IActionResult> DeleteStudent(int? id){
            if (id == null){
                return NotFound();
            }

            var student = await _context._students.FindAsync(id);
            if(student == null){
                return NotFound();
            }

            return View(student);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteStudent(int? id, int StudentId){
            if (id == null){
                return NotFound();
            }

            if(id != StudentId){
                return NotFound();
            }

            var student = await _context._students.FindAsync(id);
            if(student == null){
                return NotFound();
            }

            _context._students.Remove(student);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
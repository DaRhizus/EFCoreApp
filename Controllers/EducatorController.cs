using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreApp.Controllers
{
    public class EducatorController:Controller
    {
        private readonly DataContext _context;

        public EducatorController(DataContext context){
            _context = context;
        }

        public async Task<IActionResult> Index(){
            var educators = await _context._educators.ToListAsync();
            return View(educators);
        }

        public IActionResult CreateEducator(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEducator(Educator educator){
            if(ModelState.IsValid){
                _context._educators.Add(educator);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(educator);
        }

        public async Task<IActionResult> EducatorDetails(int? id){
            if(id == null){
                return NotFound();
            }

            var educator = await _context
                                ._educators
                                .Include(x=>x.Courses)
                                .FirstOrDefaultAsync(x=>x.EducatorId == id);
            if(educator == null){
                return NotFound();
            }

            return View(educator);
        }

        public async Task<IActionResult> EditEducator(int? id){
            if(id == null){
                return NotFound();
            }

            var educator = await _context._educators.FindAsync(id);
            if(educator == null){
                return NotFound();
            }

            return View(educator);
        }

        [HttpPost]
        public async Task<IActionResult> EditEducator(int? id, Educator educator){
            if(id == null){
                return NotFound();
            }

            if(ModelState.IsValid){
                try{
                    _context._educators.Update(educator);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch(DbUpdateConcurrencyException){
                    if(!_context._educators.Any(x => x.EducatorId == id)){
                        return NotFound();
                    }
                    else{
                        throw;
                    }
                }
            }

            return View(educator);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using EFCoreApp.Models;
using EFCoreApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EFCoreApp.Controllers
{
    public class CourseController: Controller
    {
        private readonly DataContext _context;

        public CourseController(DataContext context){
            _context = context;
        }

        public async Task<IActionResult> Index(){
            var courses = await _context
                                ._courses
                                .Include(x=>x.Educator)
                                .ToListAsync();
            return View(courses);
        }

        public async Task<IActionResult> CreateCourse(){
            ViewBag.Educators = new SelectList(await _context._educators.ToListAsync(), "EducatorId", "EducatorFullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CourseViewModel course, IFormFile ImageFile){
            var allowedExtensions = new[]{".jpg", ".jpeg", ".png",};
            
            var extension = Path.GetExtension(ImageFile.FileName);
            var imageFileName = $"{CharacterNormalizer.NormalizeTurkishChars(course.CourseName.ToLower())}{extension}";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", imageFileName);

            if(!allowedExtensions.Contains(extension)){
                ModelState.AddModelError("", "Lütfen sadece jpg/jpeg veya png türünde bir resim yükleyiniz");
            }             

            if(ModelState.IsValid){
                using(var stream = new FileStream(path, FileMode.Create)){
                    await ImageFile.CopyToAsync(stream);
                }
                course.CourseImage = imageFileName;
                _context._courses.Add(new Course{
                    CourseName = course.CourseName,
                    CourseImage = course.CourseImage,
                    EducatorId = course.EducatorId,
                });
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(course);
        }


        public async Task<IActionResult> CourseDetails(int? id){
            if(id == null){
                return NotFound();
            }

            var course = await _context
                                ._courses
                                .Include(x=>x.CourseApplies)
                                .ThenInclude(x=>x.Student)
                                .Include(x=>x.Educator)
                                .FirstOrDefaultAsync(x=>x.CourseId == id);
            if(course == null){
                return NotFound();
            }

            return View(course);
        }

        public async Task<IActionResult> EditCourse(int? id){
            if(id==null){
                return NotFound();
            }
            ViewBag.Educators = new SelectList(await _context._educators.ToListAsync(), "EducatorId", "EducatorFullName");
            var course = await _context._courses.FindAsync(id);
            var model = new CourseViewModel{
                CourseId = course.CourseId,
                CourseName = course.CourseName,
                CourseImage = course.CourseImage
            };
            if(course==null){
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditCourse(int? id, CourseViewModel model, IFormFile? ImageFile){
            if(id==null){
                return NotFound();
            }

            if(ModelState.IsValid){
                if(ImageFile!=null){
                    var allowedExtensions = new[]{".jpg", ".jpeg", ".png",};
                    var extension = Path.GetExtension(ImageFile.FileName);
                    var imageFileName = $"{CharacterNormalizer.NormalizeTurkishChars(model.CourseName.ToLower())}{extension}";
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", imageFileName);

                    if(!allowedExtensions.Contains(extension)){
                        ModelState.AddModelError("", "Lütfen sadece jpg/jpeg veya png türünde bir resim yükleyiniz");
                    }

                    if (System.IO.File.Exists(path)){
                        System.IO.File.Delete(path);
                    }
                    
                    using(var stream = new FileStream(path, FileMode.Create)){
                        await ImageFile.CopyToAsync(stream);
                    }
                    model.CourseImage = imageFileName;       
                }
                try{
                    _context.Update(new Course {
                        CourseId = model.CourseId,
                        CourseName = model.CourseName,
                        CourseImage = model.CourseImage,
                        EducatorId = model.EducatorId,
                    });
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch(Exception){
                    throw;
                }
            }
            ViewBag.Educators = new SelectList(await _context._educators.ToListAsync(), "EducatorId", "EducatorFullName");
            return View(model);

        }

        public async Task<IActionResult> DeleteCourse(int? id){
            if(id==null){
                return NotFound();
            }

            var course = await _context._courses.FindAsync(id);
            if(course==null){
                return NotFound();
            }

            return View(course);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteCourse(int? id, int CourseId, IFormFile CourseImage){
            if(id==null){
                return NotFound();
            }

            var course = await _context._courses.FindAsync(id);
            if(course==null){
                return NotFound();
            }

            if(id!=CourseId){
                return NotFound();
            }

            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", course.CourseImage);
            if (System.IO.File.Exists(imagePath)){
                System.IO.File.Delete(imagePath);
            }

            _context._courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
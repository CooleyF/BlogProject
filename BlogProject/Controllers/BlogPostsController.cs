using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BlogProject.Helpers;
using BlogProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BlogProject.Controllers
{

    [Authorize(Roles = "Admin")]
    public class BlogPostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public Task<ApplicationUser> ApplicationUser { get; private set; }

        // GET: BlogPosts
        [AllowAnonymous]
        public ActionResult Index()
        {
            var blogPosts = db.BlogPosts.Include(b => b.Author);
            return View(blogPosts.Where(b => b.Published).ToList());
        }

        // GET: BlogPosts/Details/5
        [AllowAnonymous]
        public ActionResult Details(string Slug)
        {
            if (String.IsNullOrWhiteSpace(Slug))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.BlogPosts.FirstOrDefault(p => p.Slug == Slug);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Create
        
        public ActionResult Create()
        {

            ViewBag.AuthorID = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: BlogPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "Title,BlogBody,Published,Abstract")] BlogPost blogPost, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                //perform validation on image

                if (ImageUploadValidator.IsWebFriendlyImage(image))
                {
                   
                    
                        var fileName = Path.GetFileName(image.FileName);
                        image.SaveAs(Path.Combine(Server.MapPath("~/Uploads/"), fileName));
                        blogPost.MediaURL = "/Uploads/" + fileName;
                   
                    
                }

                //Validate slug 
                var slug = StringUtilities.URLFriendly(blogPost.Title);
                if (String.IsNullOrWhiteSpace(slug))
                {
                    ModelState.AddModelError("Title", "Invalid title");
                    return View(blogPost);
                }
                if (db.BlogPosts.Any(p => p.Slug == slug))
                {
                    ModelState.AddModelError("Title", "The title must be unique");
                    return View(blogPost);
                }

                
                
                
            
           


            /*creates user manager*/
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));


            blogPost.Slug = slug;
            blogPost.AuthorID = userManager.FindByName(User.Identity.Name).Id;
            blogPost.Created = DateTimeOffset.Now;

            db.BlogPosts.Add(blogPost);
            db.SaveChanges();

                return RedirectToAction("Index");
             }

                ViewBag.AuthorID = new SelectList(db.Users, "Id", "FirstName", blogPost.AuthorID);
            return View(blogPost);
        }

        // GET: BlogPosts/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.BlogPosts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorID = new SelectList(db.Users, "Id", "FirstName", blogPost.AuthorID);
            return View(blogPost);
        }

        //public ActionResult EditWithSlug(string slug)
        //{
        //    if(!string.IsNullOrEmpty(slug))
        //    {
        //        BlogPost post = db.BlogPosts.FirstOrDefault(r => r.Slug == slug);
        //        if(post != null)
        //        {
        //            return View("Edit.cshtml", post);
        //        }
        //    }

        //    return RedirectToAction("Index");
        //}

        // POST: BlogPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Edit([Bind(Include = "ID,AuthorID,Created,Updated,Title,Slug,BlogBody,MediaURL,Published,Abstract")] BlogPost blogPost, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                
               



                if (ImageUploadValidator.IsWebFriendlyImage(image))
                {
                    
                    
                        var fileName = Path.GetFileName(image.FileName);
                        image.SaveAs(Path.Combine(Server.MapPath("~/Uploads/"), fileName));
                        blogPost.MediaURL = "~/Uploads/" + fileName;
                    
                   
                }

                var slug = StringUtilities.URLFriendly(blogPost.Title);
                if (blogPost.Slug != slug)
                {
                    if (String.IsNullOrWhiteSpace(slug))
                    {
                        ModelState.AddModelError("Title", "Invalid title");
                        return View(blogPost);
                    }
                    if (db.BlogPosts.Any(p => p.Slug == slug))
                    {
                        ModelState.AddModelError("Title", "The title must be unique");
                        return View(blogPost);
                    }
                }

                blogPost.Updated = DateTimeOffset.Now;
                db.Entry(blogPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            
            ViewBag.AuthorID = new SelectList(db.Users, "Id", "FirstName", blogPost.AuthorID);
            return View(blogPost);
        }

        // GET: BlogPosts/Delete/5
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.BlogPosts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPost blogPost = db.BlogPosts.Find(id);
            db.BlogPosts.Remove(blogPost);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

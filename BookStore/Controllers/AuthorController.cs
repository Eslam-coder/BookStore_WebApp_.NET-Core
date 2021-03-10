using BookStore.Models;
using BookStore.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    //[Authorize (Roles ="Admin")]
    public class AuthorController : Controller
    {
        private readonly Models.Repositories.IRepository<Author> bookRepository;

        public AuthorController(IRepository<Author> bookRepository)
        {
            this.bookRepository = bookRepository;
        }
        // GET: AuthorController
        public ActionResult Index()
        {
          IList<Author> ListAuthor = bookRepository.Get();
            return View(ListAuthor);
        }

        // GET: AuthorController/Details/5
        public ActionResult Details(int id)
        {
            Author AuthorInList =  bookRepository.Find(id);
            return View(AuthorInList);
        }

        // GET: AuthorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author newAuthor)
        {
            if (ModelState.IsValid)
            {
                bookRepository.Add(newAuthor);
                return RedirectToAction("Index");
            }
            return View();
            
        }

        // GET: AuthorController/Edit/5
        public ActionResult Edit(int id)
        {
            Author AuthorInList = bookRepository.Find(id);
            return View(AuthorInList);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Author NewAuthor)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            Author AuthorInList = bookRepository.Find(id);
            if (AuthorInList == null)
            {
                return NotFound();
            }
            AuthorInList.ID = NewAuthor.ID;
            AuthorInList.FullName = NewAuthor.FullName;
            return RedirectToAction("Index");
          
        }

        // GET: AuthorController/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            Author AuthorInList = bookRepository.Find(id);
            if (AuthorInList == null)
            {
                return NotFound();
            }

            return View(AuthorInList);
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
           
            bookRepository.Delete(id);
            return RedirectToAction("Index");
           
        }
    }
}

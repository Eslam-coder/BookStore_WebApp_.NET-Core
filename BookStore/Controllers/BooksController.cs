using BookStore.Models;
using BookStore.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace BookStore.Controllers
{
    public class BooksController : Controller
    {
        private readonly Models.Repositories.IRepository<Book> bookRepository;

        private readonly Models.Repositories.IRepository<Author> authorRepository;

        public  IHostingEnvironment Hosting { get; }

        public BooksController(IRepository<Book> bookRepository, IRepository<Author> authorRepository,IHostingEnvironment hosting)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.Hosting = hosting;
        }

        // GET: BooksController
        public ActionResult Index()
        {
            IList<Book> BookList = bookRepository.Get();
            return View(BookList);
        }

        // GET: BooksController/Details/5
        public ActionResult Details(int id)
        {
            Book BookInList = bookRepository.Find(id);
            if(BookInList == null)
            {
                int BookId = id;
                return View("BookNotFound", BookId);
            }
            return View(BookInList);
        }

        // GET: BooksController/Create
        public ActionResult Create()
        {
            var BookAuthorViewModel = new BookAuthorViewModel
            {
                Author = authorRepository.Get().ToList()
            };
            return View(BookAuthorViewModel);
        }

        // POST: BooksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel newBookAuthor)
        {
            //Saving File Or Image
            string uploads = Path.Combine(Hosting.WebRootPath, "Uploads");
            string FileName = newBookAuthor.File.FileName;
            string FullPath = Path.Combine(uploads, FileName);
            newBookAuthor.File.CopyTo(new FileStream(FullPath, FileMode.Create));

            var author = authorRepository.Find(newBookAuthor.Book.Author.ID);
            var Book = newBookAuthor.Book;
            Book newBook = new Book
            {
                ID = Book.ID,
                Title = Book.Title,
                Description = Book.Description,
                Author = author,
                //Saving File Or Image
                ImageUrl = FileName
            };
            bookRepository.Add(newBook);
            return RedirectToAction("Index");
           
        }

        // GET: BooksController/Edit/5
        public ActionResult Edit(int id)
        {
            Book BookInList = bookRepository.Find(id);
            //Author Author =  authorRepository.Find(id);
            var BookAuthorInList = new BookAuthorViewModel
            {
                Book = BookInList,
                Author = authorRepository.Get().ToList(),
                
            };
            return View(BookAuthorInList);
        }

        // POST: BooksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookAuthorViewModel UpdateBookAuthor)
        {
            //Saving File Or Image
            string uploads = Path.Combine(Hosting.WebRootPath, "Uploads");
            string FileName = UpdateBookAuthor.File.FileName;
            string FullPath = Path.Combine(uploads, FileName);

           
            // Delete The Old Path
            string OldFileName = bookRepository.Find(id).ImageUrl;
            string OLdPath = Path.Combine(uploads, OldFileName);

            if (FullPath != OLdPath)
            {
                System.IO.File.Delete(OLdPath);

                //Save The New File or New Image 
                UpdateBookAuthor.File.CopyTo(new FileStream(FullPath, FileMode.Create));
            }
          
            var author = authorRepository.Find(UpdateBookAuthor.Book.Author.ID);
            var Book = UpdateBookAuthor.Book;
            Book UpdateBook = new Book
            {
                Title = Book.Title,
                Description = Book.Description,
                Author = author,
                ImageUrl = FileName
            };
            bookRepository.Update(id, UpdateBook);
            return RedirectToAction("Index");

        }

        // GET: BooksController/Delete/5
        public ActionResult Delete(int id)
        {
            Book bookInList = bookRepository.Find(id);
            return View(bookInList);
        }

        // POST: BooksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            bookRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Search(string term)
        {
            var Result = bookRepository.Search(term);
            return View("Index", Result);
        }
    }
}

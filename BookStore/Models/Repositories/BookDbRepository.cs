using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repositories
{
    public class BookDbRepository : IRepository<Book>
    {
        BookStoreContext context;
        //BookStoreContext context = new BookStoreContext();
        public BookDbRepository(BookStoreContext _context)
        {
            context = _context;
        }
        //Create , Post
        public void Add(Book newBook)
        {
            context.Books.Add(newBook);
            context.SaveChanges();
        }

        //Delete
        public void Delete(int id)
        {
            Book bookInDb = context.Books.FirstOrDefault(b => b.ID == id);
            context.Books.Remove(bookInDb);
            context.SaveChanges();
        }

        //GetById 
        public Book Find(int id)
        {
            Book bookInDb = context.Books.FirstOrDefault(b => b.ID == id);
            return bookInDb;
        }

        //Get
        public IList<Book> Get()
        {
            return context.Books.Include(b=>b.Author).ToList();
        }

        public List<Book> Search(string term)
        {
            var ResultSearch = context.Books.Include(b=>b.Author).Where(b=>b.Title.Contains(term)|| b.Description.Contains(term)||b.Author.FullName.Contains(term)).ToList();
            return ResultSearch;
        }

        //Put , Update
        public void Update(int id, Book NewBook)
        {
            Book bookInList = context.Books.FirstOrDefault(b => b.ID == id);
            bookInList.Title = NewBook.Title;
            bookInList.Description = NewBook.Description;
            bookInList.Author = NewBook.Author;
            bookInList.ImageUrl = NewBook.ImageUrl;
            context.SaveChanges();
        }
    }
}



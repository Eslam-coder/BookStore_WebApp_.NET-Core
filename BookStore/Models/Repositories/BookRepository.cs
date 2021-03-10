using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repositories
{
    public class BookRepository : IRepository<Book>
    {
        List<Book> ListBook = new List<Book>()
        {
            new Book{ID=1,Title="C#",Description="Programming",Author=new Author(){ ID=1,FullName="Ahmed"},ImageUrl="Image02.jpg" },
            new Book{ID=2,Title="Java",Description="Programming",Author=new Author(){ ID=1,FullName="Ahmed"},ImageUrl="Image03.jpg"},
            new Book{ID=3,Title="Python",Description="Programming",Author=new Author(){ ID=2,FullName="Ali"},ImageUrl="Image04.jpg"}
        };

        //Create , Post
        public void Add(Book newBook)
        {
            newBook.ID = ListBook.Max(b => b.ID) + 1;
            ListBook.Add(newBook);
        }

        //Delete
        public void Delete(int id)
        {
            Book bookInListBook =   ListBook.FirstOrDefault(b => b.ID == id);
            ListBook.Remove(bookInListBook);
        }

        //GetById 
        public Book Find(int id)
        {
            Book bookInList = ListBook.FirstOrDefault(b => b.ID == id);
            return bookInList;
        }

        //Get
        public IList<Book> Get()
        {
            return ListBook;
        }

        public List<Book> Search(string term)
        {
            throw new NotImplementedException();
        }

        //Put , Update
        public void Update(int id, Book NewBook)
        {
            Book bookInList = ListBook.FirstOrDefault(b => b.ID == id);
            bookInList.Title = NewBook.Title;
            bookInList.Description = NewBook.Description;
            bookInList.Author = NewBook.Author;
            bookInList.ImageUrl = NewBook.ImageUrl;
        }
    }
}

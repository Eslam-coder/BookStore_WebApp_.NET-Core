using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Models.Repositories
{
    public class AuthorDbRepository : IRepository<Author>
    {
        BookStoreContext context;
        public AuthorDbRepository(BookStoreContext _context)
        {
            context = _context;
        }
        

        //Post
        public void Add(Author NewAuthor)
        {
            context.Authors.Add(NewAuthor);
        }

        //Delete
        public void Delete(int id, Author RemoveAuthor)
        {
            Author authorInDb = context.Authors.FirstOrDefault(a => a.ID == id);
            context.Authors.Remove(authorInDb);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        //GetById
        public Author Find(int id)
        {
            Author authorInDb = context.Authors.FirstOrDefault(a => a.ID == id);
            return authorInDb;
        }

        //Get
        public IList<Author> Get()
        {
            return context.Authors.ToList();
        }

        public List<Author> Search(string term)
        {
            var ResultSearch = context.Authors.Where(b => b.FullName.Contains(term)).ToList();
            return ResultSearch;
        }

        //Put 
        public void Update(int id, Author newAuthor)
        {
            Author authorInDb = context.Authors.FirstOrDefault(a => a.ID == id);
            authorInDb.FullName = newAuthor.FullName;
            context.SaveChanges();
        }
    }
}



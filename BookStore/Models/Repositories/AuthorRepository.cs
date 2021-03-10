using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repositories
{
    public class AuthorRepository : IRepository<Author>
    {
        List<Author> ListAuthor = new List<Author>()
        {
            new Author{ID=1,FullName="Ahmed"},
            new Author{ID=2,FullName="Ali"}
        };

        //Post
        public void Add(Author NewAuthor)
        {
            NewAuthor.ID = ListAuthor.Max(a => a.ID) + 1;
            ListAuthor.Add(NewAuthor);
        }

        //Delete
        public void Delete(int id , Author RemoveAuthor)
        {
            Author authorInListAuthor =  ListAuthor.FirstOrDefault(a => a.ID == id);
            ListAuthor.Remove(authorInListAuthor);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        //GetById
        public Author Find(int id)
        {
            Author authorInListAuthor = ListAuthor.FirstOrDefault(a => a.ID == id);
            return authorInListAuthor;
        }

        //Get
        public IList<Author> Get()
        {
            return ListAuthor;
        }

        public List<Author> Search(string term)
        {
            throw new NotImplementedException();
        }

        //Put 
        public void Update(int id, Author newAuthor)
        {
            Author authorInListAuthor = ListAuthor.FirstOrDefault(a => a.ID == id);
            authorInListAuthor.FullName = newAuthor.FullName;
        }
    }
}

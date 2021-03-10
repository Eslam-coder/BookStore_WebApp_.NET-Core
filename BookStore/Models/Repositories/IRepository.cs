using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repositories
{
    public interface IRepository<TEntity>
    {
        //Get
        IList<TEntity> Get();

        //GetById
        TEntity Find(int id);

        //Post , Create
        void Add(TEntity entity);

        //Delete
        void Delete(int id);

        //Update , Put
        void Update(int id, TEntity entity);

        //Search
        List<TEntity> Search(string term);
    }
}

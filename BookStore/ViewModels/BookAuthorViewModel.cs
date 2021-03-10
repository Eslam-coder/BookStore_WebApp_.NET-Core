using BookStore.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class BookAuthorViewModel
    {
        public Book Book { get; set; }
        public List<Author> Author { get; set; }
        public IFormFile File { get; set; }
    }
}

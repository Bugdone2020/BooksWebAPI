using System;

namespace BooksWebAPI
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int PagesCount { get; set; }
    }
}

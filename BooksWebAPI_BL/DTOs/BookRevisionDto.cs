using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksWebAPI_BL.DTOs
{
    public class BookRevisionDto
    {
        public int YearOfPublishing { get; set; }
        public int PagesCount { get; set; }
        public float Price { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksWebAPI_DAL.Entities
{
    public class Point : BaseEntity
    {
        public float Longitude { get; set; }
        public float Latitude { get; set; }
    }
}

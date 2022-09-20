using BooksWebAPI_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksWebAPI_BL.Services.LibraryService
{
    public interface ILibraryService
    {
        Task<IEnumerable<Library>> GetNearestLibraries(
            Point userLocation, 
            int count = 10);
    }
}

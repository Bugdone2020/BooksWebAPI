using BooksWebAPI_DAL;
using BooksWebAPI_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksWebAPI_BL.Services.LibraryService
{
    public class LibraryService : ILibraryService
    {
        IGenericRepository<Library> _genericLibraryRepository;
        public LibraryService(IGenericRepository<Library> genericLibraryRepository)
        {
            _genericLibraryRepository = genericLibraryRepository;
        }
        public async Task<IEnumerable<Library>> GetNearestLibraries(
            Point userLocation, int count = 10)
        {
            return await _genericLibraryRepository.GetAllByPredicate(x => 
            Math.Sqrt(
                Math.Pow(10 - x.Location.Latitude, 2)
                +
                Math.Pow(5 - x.Location.Longitude, 2)) < 10 );
        }

        //TODO : 
        //public async Task<IEnumerable<Library>> GetNearestLibraries(
        //    Point userLocation, int count = 10)
        //{
        //    return await _genericLibraryRepository.GetAllByPredicate(x => Filter(x, userLocation));
        //}
        //private bool Filter(Library library, Point userLocation)
        //{
        //    var distance = Math.Sqrt(
        //        Math.Pow(userLocation.Latitude - library.Location.Latitude, 2)
        //        +
        //        Math.Pow(userLocation.Longitude - library.Location.Longitude, 2));
        //    return distance < 10;
        //}
    }
}

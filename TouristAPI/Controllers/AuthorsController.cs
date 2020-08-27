using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseLibrary.API.DbContexts;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using TouristAPI.Helpers;
using TouristAPI.Models;

namespace TouristAPI.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        //private readonly IMapper _mapper;

        public AuthorsController(ICourseLibraryRepository courseLibraryRepository)
        {
            _courseLibraryRepository = courseLibraryRepository ??
                            throw new ArgumentNullException(nameof(courseLibraryRepository));
            //    _mapper = mapper ??
            //        throw new ArgumentNullException(nameof(mapper));
            //
        }

        /// <summary>
        /// Gets all authors.
        /// </summary>
        /// <remarks>
        /// Sample request: get/authors
        /// </remarks>
        /// <returns>The list of authors
        /// </returns>
        /// 
        [HttpGet()]
        //[Produces("application/json")]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors()
        {
            var authorsFromRepo = _courseLibraryRepository.GetAuthors();
            var authors = new List<AuthorDto>();

            foreach (var author in authorsFromRepo)
            {
                authors.Add(new AuthorDto()
                {
                    Id = author.Id,
                    Name = $"{author.FirstName} {author.LastName}",
                    MainCategory = author.MainCategory,
                    Age = author.DateOfBirth.GetCurrentAge()
                });
                ;
            }


            return Ok(authors);
        }


        ///<summary>
        ///Get an author by id.
        /// </summary>
        /// <remarks>
        /// Sample request: Get /authors/{id}
        /// </remarks>
        /// <returns>An individual author
        /// </returns>
        [HttpGet("{authorid}")]
        //[Produces("application/json")]
        public IActionResult GetAuthor(Guid authorId)
        {
            var author = _courseLibraryRepository.GetAuthor(authorId);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

    }
}
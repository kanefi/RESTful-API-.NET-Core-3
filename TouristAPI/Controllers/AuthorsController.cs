﻿using AutoMapper;
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
using TouristAPI.ResourceParameters;

namespace TouristAPI.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public AuthorsController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ??
                            throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

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
        [HttpHead]
        //[Produces("application/json")]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors(
            AuthorsResourceParameters authorsResourceParameters)
        {
            var authorsFromRepo = _courseLibraryRepository.GetAuthors(authorsResourceParameters);
            

            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo));
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
            var authorFromRepo = _courseLibraryRepository.GetAuthor(authorId);

            if (authorFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AuthorDto>(authorFromRepo));
        }

    }
}
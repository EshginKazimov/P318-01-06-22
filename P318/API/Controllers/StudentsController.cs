using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DomainModel.Dto;
using DomainModel.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Context;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public StudentsController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var students = await _dbContext.Students.ToListAsync();

            return Ok(_mapper.Map<List<StudentDto>>(students));
        }

        //DTO - Data Transfer Object
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int? id)
        {
            if (id == null)
                return BadRequest();

            var student = await _dbContext.Students.FindAsync(id);
            if (student == null)
                return NotFound("Bele id-de student yoxdur.");

            var studentDto = _mapper.Map<StudentDto>(student);

            return Ok(studentDto);
        }
    }
}

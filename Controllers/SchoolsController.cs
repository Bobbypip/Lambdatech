using Lambdatech.Dtos;
using Lambdatech.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lambdatech.Controllers
{
    [ApiController]
    [Route("schools")]
    public class SchoolsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public SchoolsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SchoolDto>>> Get()
        {
            var entities = await context.Schools.ToListAsync();
            var entitiesList = new List<SchoolDto>();

            foreach (var entity in entities)
            {
                entitiesList.Add(new SchoolDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    StudentId = entity.StudentId
                });
            }

            return entitiesList;
        }

        [HttpGet("{id:int}", Name = "getSchool")]
        public async Task<ActionResult<SchoolDto>> Get(int id)
        {
            var entity = await context.Schools.FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return NotFound();
            }

            return new SchoolDto
            {
                Id = entity.Id,
                Name = entity.Name,
                StudentId = entity.StudentId
            };
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] School school)
        {
            var id = school.StudentId;
            var student = await context.Students.FirstOrDefaultAsync(x => x.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            context.Add(school);
            await context.SaveChangesAsync();

            return new CreatedAtRouteResult("getSchool", new { id = school.Id }, school);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<School>> Delete(int id)
        {
            var entity = await context.Schools.FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return NotFound();
            }

            context.Remove(entity);
            await context.SaveChangesAsync();
            return Ok(entity);
        }
    }
}

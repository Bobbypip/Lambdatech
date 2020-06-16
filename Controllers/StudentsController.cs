using Lambdatech.Dtos;
using Lambdatech.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Lambdatech.Controllers
{
    [ApiController]
    [Route("students")]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public StudentsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<StudentDto>>> Get()
        {
            var entities = await context.Students.Include(x => x.Dad).Include(x => x.Mom).Include(x => x.Schools).ToListAsync();
            var entitiesList = new List<StudentDto>();
            

            foreach (var entity in entities)
            {
                var schoolList = new List<SchoolDto>();

                foreach (var school in entity.Schools)
                {
                    schoolList.Add(new SchoolDto {
                        Id = school.Id,
                        Name = school.Name
                    });
                }
                entitiesList.Add(new StudentDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    MomId = entity.MomId,
                    MomName = entity.Mom.Name,
                    DadId = entity.DadId,
                    DadName = entity.Dad.Name,
                    Schools = schoolList
                });
            }
            return entitiesList;
        }

        [HttpGet("{id:int}/{name?}", Name = "getStudent")]
        public async Task<ActionResult<StudentDto>> Get(int id, string name)
        {
            var schoolList = new List<SchoolDto>();

            if (name != null)
            {
                var entityname = await context.Students.Include(x => x.Dad).Include(x => x.Mom).Include(x => x.Schools).FirstOrDefaultAsync(x => x.Name == name);

                if (entityname == null)
                {
                    return NotFound();
                }
                else
                {
                    foreach (var school in entityname.Schools)
                    {
                        schoolList.Add(new SchoolDto
                        {
                            Id = school.Id,
                            Name = school.Name
                        });
                    }

                    return new StudentDto
                    {
                        Id = entityname.Id,
                        Name = entityname.Name,
                        MomId = entityname.MomId,
                        MomName = entityname.Mom.Name,
                        DadId = entityname.DadId,
                        DadName = entityname.Dad.Name,
                        Schools = schoolList
                    };
                }
            }
            else
            {
                var entity = await context.Students.Include(x => x.Dad).Include(x => x.Mom).Include(x => x.Schools).FirstOrDefaultAsync(x => x.Id == id);

                if (entity == null)
                {
                    return NotFound();
                }
                else
                {
                    foreach (var school in entity.Schools)
                    {
                        schoolList.Add(new SchoolDto
                        {
                            Id = school.Id,
                            Name = school.Name
                        });
                    }

                    return new StudentDto
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        MomId = entity.MomId,
                        MomName = entity.Mom.Name,
                        DadId = entity.DadId,
                        DadName = entity.Dad.Name,
                        Schools = schoolList
                    };
                }

            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Student student)
        {
            context.Add(student);
            await context.SaveChangesAsync();

            return new CreatedAtRouteResult("getStudent", new { id = student.Id }, student);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Student student)
        {
            if (student.Id != id)
            {
                return BadRequest();
            }

            student.Id = id;
            context.Entry(student).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Student>> Delete(int id)
        {
            var entity = await context.Students.FirstOrDefaultAsync(x => x.Id == id);

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

using Lambdatech.Dtos;
using Lambdatech.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Lambdatech.Controllers
{
    [ApiController]
    [Route("dads")]
    public class DadsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public DadsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<DadDto>>> Get()
        {
            var entities = await context.Dads.ToListAsync();
            var entitiesList = new List<DadDto>();

            foreach (var entity in entities)
            {
                entitiesList.Add(new DadDto
                {
                    Id = entity.Id,
                    Name = entity.Name
                });
            }

            return entitiesList;
        }

        [HttpGet("{id:int}/{name?}", Name = "getDad")]
        public async Task<ActionResult<DadDto>> Get(int id, string name)
        {
            if (name != null)
            {
                var entityname = await context.Dads.FirstOrDefaultAsync(x => x.Name == name);

                if (entityname == null)
                {
                    return NotFound();
                }
                else
                {
                    return new DadDto
                    {
                        Id = entityname.Id,
                        Name = entityname.Name
                    };
                }
            }
            else
            {
                var entity = await context.Dads.FirstOrDefaultAsync(x => x.Id == id);

                if (entity == null)
                {
                    return NotFound();
                }

                return new DadDto
                {
                    Id = entity.Id,
                    Name = entity.Name
                };
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Dad dad)
        {
            context.Add(dad);
            await context.SaveChangesAsync();

            return new CreatedAtRouteResult("getDad", new { id = dad.Id }, dad);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Dad>> Delete(int id)
        {
            var entity = await context.Dads.FirstOrDefaultAsync(x => x.Id == id);

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

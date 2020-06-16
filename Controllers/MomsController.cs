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
    [Route("moms")]
    public class MomsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public MomsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<MomDto>>> Get()
        {
            var entities = await context.Moms.ToListAsync();
            var entitiesList = new List<MomDto>();

            foreach (var entity in entities)
            {
                entitiesList.Add(new MomDto
                {
                    Id = entity.Id,
                    Name = entity.Name
                });
            }

            return entitiesList;
        }

        [HttpGet("{id:int}/{name?}", Name = "getMom")]
        public async Task<ActionResult<MomDto>> Get(int id, string name)
        {
            if (name != null)
            {
                var entityname = await context.Moms.FirstOrDefaultAsync(x => x.Name == name);

                if (entityname == null)
                {
                    return NotFound();
                }
                else
                {
                    return new MomDto
                    {
                        Id = entityname.Id,
                        Name = entityname.Name
                    };
                }
            }
            else
            {
                var entity = await context.Moms.FirstOrDefaultAsync(x => x.Id == id);

                if (entity == null)
                {
                    return NotFound();
                }

                return new MomDto
                {
                    Id = entity.Id,
                    Name = entity.Name
                };
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Mom mom)
        {
            context.Add(mom);
            await context.SaveChangesAsync();

            return new CreatedAtRouteResult("getMom", new { id = mom.Id }, mom);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Mom>> Delete(int id)
        {
            var entity = await context.Moms.FirstOrDefaultAsync(x => x.Id == id);

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

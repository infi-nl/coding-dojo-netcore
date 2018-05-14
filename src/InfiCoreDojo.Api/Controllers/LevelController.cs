using System;
using System.Collections.Generic;
using System.Linq;
using InfiCoreDojo.DataAccess;
using InfiCoreDojo.Domain;
using Microsoft.AspNetCore.Mvc;

namespace InfiCoreDojo.Api.Controllers
{
    // TODO: Authorization
    [Route("api/v1/[controller]")]
    public class LevelController : Controller
    {
        private readonly ILevelDal levelDal;

        public LevelController(ILevelDal levelDal)
        {
            this.levelDal = levelDal ?? throw new ArgumentNullException(nameof(levelDal));
        }

        [HttpGet("")]
        public IEnumerable<Level> GetAll()
        {
            return levelDal.Query().ToList();
        }
        
        [HttpGet("{id}")]
        public Level Get(Guid id)
        {
            return levelDal.Get(id);
        }
        
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody]Level level)
        {
            if (id != level.Id)
            {
                throw new InvalidOperationException($"Uri contained id '{id}', which is different from the submitted level id '{level.Id}'");
            }

            levelDal.Upsert(level);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            levelDal.Delete(id);
        }
    }
}

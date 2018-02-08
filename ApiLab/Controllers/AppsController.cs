using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiLab.Models;

namespace ApiLab.Controllers
{
    /// <summary>
    /// The class handles api/apps/* API logic.
    /// TODO: Add server authentication.
    /// </summary>
    [Route("api/[controller]")]
    public class AppsController : Controller
    {
        private ApiLabDatabaseContext dbContext;

        public AppsController(ApiLabDatabaseContext context)
        {
            dbContext = context;
        }

        // GET api/apps
        [HttpGet]
        public IEnumerable<App> Get()
        {
            var apps = dbContext.Apps.ToList();
            return apps;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}

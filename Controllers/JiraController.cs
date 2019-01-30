using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiGTT.Models;

namespace ApiGTT.Controllers

{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class JiraController : ControllerBase
    {
           private readonly AppDBContext _context;
          public JiraController(AppDBContext context)
        {
            this._context = context;
            if(this._context.Jira.Count()==0)
            {
                    Console.WriteLine("No existe usuarios");
                   Jira jira = new Jira();
                    jira.username="Xavi";
                    jira.password="pass";
                    this._context.Jira.Add(jira);
                    this._context.SaveChanges();
            }
        }

        
        // GET api/Jira
        [HttpGet]
        public ActionResult<List<Jira>> Get()
        {
            return this._context.Jira.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

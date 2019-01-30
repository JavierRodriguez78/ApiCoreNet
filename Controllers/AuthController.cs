using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using ApiGTT.Models;
using ApiGTT.Helpers;

namespace ApiGTT.Controllers
{
     [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly AppDBContext _context;
        // GET api/values

        public AuthController(AppDBContext context){
            this._context= context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] Users value)
        {
            try{
                
             Users UserResult = this._context.Users.Where(
                user=>user.username==value.username).First();
             if(UserResult.password == Encrypt.Hash(value.password)){
                var jsonResult=new { Id = "23", Name = "Scott"};
                return Ok(jsonResult);
             }else{
                 return Unauthorized();
             }
                
            }catch(Exception e){
                return NotFound();
            
            }
            
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

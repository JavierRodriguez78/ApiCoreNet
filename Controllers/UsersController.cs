using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiGTT.Models;

namespace ApiGTT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersContext _context;

        public UsersController(UsersContext context)
        {
            this._context = context;
            if(this._context.Users.Count()==0)
            {
                    Console.WriteLine("No existe usuarios");
                    Users usuario = new Users();
                    usuario.username="Xavi";
                    usuario.password="pass";
                    this._context.Users.Add(usuario);
                    this._context.SaveChanges();
            }
        }

        // GET api/users
        [HttpGet]
        public ActionResult<List<Users>> GetAll()
        {
            return this._context.Users.ToList();
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public ActionResult<Users> Get(long id)
        {
            Users user = this._context.Users.Find(id);
            if (user== null){
                return NotFound();
            }
            return user;
        }

        // POST api/values
        [HttpPost]
        public ActionResult<Users> Post([FromBody] Users value)
        {
                    this._context.Users.Add(value);
                    this._context.SaveChanges();
                    return Ok(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(long id, [FromBody] Users value)
        {
            Users user = this._context.Users.Find(id);
            user.username = value.username;
            user.password = value.password;
            this._context.SaveChanges();
        }

       
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(long id)
        {
            Users userEliminar = this._context.Users.Where(
                user=>user.id==id).First();
                if(userEliminar == null ){
                    return "No existe usuario";
                }
           this._context.Remove(userEliminar);
           this._context.SaveChanges();
           return "Se ha borrado ->"+userEliminar.id;
        }
    }
}

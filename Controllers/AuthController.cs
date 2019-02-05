using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web;

using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
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
                
                Console.WriteLine("Recibido en el post ->>>>"+ value.username);
                
             Users UserResult = this._context.Users.Where(
                user=>user.username==value.username).First();
             if(UserResult.password == Encrypt.Hash(value.password)){
                Console.WriteLine("Login");
                String token = BuildToken(UserResult);
                return Ok(new {token= token});
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

        public String BuildToken(Users data)
        {
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
    	    var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
		    {
			    Subject = new ClaimsIdentity(new[]
			    {
				    new Claim( "UserID", data.id.ToString() ),
				    new Claim( "UserName", data.username ),
				 }),
			    Expires = now.AddDays(1),
			    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretsecretsecret")), SecurityAlgorithms.HmacSha256),
		    };		
		    var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
			return tokenString;
	    }

    }

    
}

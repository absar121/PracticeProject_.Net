using CR1.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Polly;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cors;
using log4net.Repository.Hierarchy;
using Microsoft.Data.SqlClient;
using System.Data;
using Google.Protobuf.WellKnownTypes;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data.Common;
using CR1.storeproce;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;
using System.Net.Http;
using System.IdentityModel.Tokens.Jwt;

namespace CR1.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class UsersController : ControllerBase
    {

        private readonly usercontext _context;
        private readonly IConfiguration _config;
        private readonly ILogger<UsersController> logger;
        public readonly datasp clas;
        private readonly storeproc proc;

        public UsersController(usercontext context, ILogger<UsersController> logger, storeproc proc) {
            _context = context;
            this.logger = logger;
            this.proc = proc ?? throw new ArgumentNullException(nameof(proc));
        }
        ////Get:api/user
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<user>>> Getuser()
        //{
        //    logger.LogInformation("getting all user");
        //    return await _context.user.ToListAsync();

        //}
        //Get:api/user   Store procedure

        //[Authorize(Roles = "admin,user")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<storeproc.users>>> Getuser()
        {
            try
            {
                var response = await proc.Getuser();
                if (response == null) { return NotFound(); }
                return  response; 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        //[Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<List<storeproc.users>> Getuserid(int id)
        {
            try
            {
                var response = await proc.GetById(id);
                if (response == null) { return response = null; }
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }  
        }

        //[Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Putuser(int id, storeproc.users user)
        {
            if (user == null)
            {
                logger.LogWarning("NULL VALUES");
                Console.WriteLine("error");
            }
            await proc.update(id,user);

            return NoContent();
        }
        //[Authorize(Roles = "admin,user")]
        [HttpPost]
        public async Task<ActionResult<storeproc.users>> Postuser(storeproc.users user)
        {
            if (user == null)
            {
                logger.LogWarning("NULL VALUES");
                Console.WriteLine("error");
            }
             await proc.insert(user);
            return Ok();

        }

        //[Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<List<storeproc.users>> Deleteuser(int id)
        {
            var user = await proc.GetById(id);

            if (user == null)
            { return null; }
            else
            {
                await proc.delete(id);
                return user;
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public  IActionResult Login(login user)
        {
            var loginvar = _context.login.Where(v => v.email == user.email && v.password == user.password).FirstOrDefault();
            var role = _context.login.Where(v => v.email == user.email && v.password == user.password).Select(v => v.role).FirstOrDefault();
            ClaimsIdentity identity = null;
            bool isauthenticated = false;
            if (loginvar != null)
            {
                if(role!=null)
                {
                    identity = new ClaimsIdentity(new[]
                    {
                         new Claim(ClaimTypes.Name,user.email),
                         new Claim(ClaimTypes.Role,role)
                     }, CookieAuthenticationDefaults.AuthenticationScheme);
                    isauthenticated = true;
                }
                if(isauthenticated)
                {
                    var princial = new ClaimsPrincipal(identity);
                    //var client = new HttpClient();
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "YOUR_AUTH_TOKEN");

                    var login =  HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,princial);
                }
                logger.LogWarning($"user with {loginvar.email} not found");
                return Ok(new jwtservice().GenerateToken(loginvar.email, loginvar.password,role));
            }
            else
            {
                return Ok("Failure"); 
                }
        }

        [AllowAnonymous]
        [HttpGet("loginstore")]
        //public async Task Getloginsp()
         public async Task<List<storeproc.Value>> Getloginsp() 
        {
            try
            {
                var response=await proc.GetAll();
            return response.ToList();
            }
            catch(Exception e)
            {
             Console.WriteLine(e.Message);
               return null;
             }
            // logger.LogInformation("getting login data by store procedure");
            // var loginsp =(await _context.login.FromSqlRaw($"userss").ToListAsync());
            // return null;

        }
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Putuser(int id,user user)
        //{
        //    if (id != user.userid)
        //    {
        //        logger.LogWarning($"user with {id} not found");
        //        return BadRequest();
        //    }

        //    _context.Entry(user).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException) { 
        //        if (id !=user.userid)
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //[AllowAnonymous]
        //[HttpGet("getbyid")]
        //public async Task<List<storeproc.Value>> Get(int id)
        //{
        //    try
        //    {
        //        var response = await proc.GetById(id);
        //        //if (response == null) { return NotFound(); }
        //        return response;

        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        return null;
        //    }

        //}

        // [AllowAnonymous]
        // [HttpGet("loginssql")]
        // public Task<ActionResult<Any>> Getloginssql()
        // {

        // logger.LogInformation("getting login data by store procedure");
        // var loginsp =(await _context.login.FromSqlRaw($"userss").ToListAsync());
        // return null;

        // }

        //[HttpPost]
        //public async Task<ActionResult<user>> Postuser(user user)
        //{
        //    if (user == null)
        //    {
        //        logger.LogWarning("NULL VALUES");
        //        Console.WriteLine("error");
        //    }
        //        _context.user.Add(user);
        //        await _context.SaveChangesAsync();
        //        return CreatedAtAction("Getuser", new { user.userid }, user);   
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult<user>> Deleteuser(int id)
        //{
        //    var user = await _context.user.FindAsync(id);
        //        if(user == null)
        //    { return NotFound();} 
        //        _context.user.Remove(user);
        //    await _context.SaveChangesAsync();
        //    return user;
        //}

    }

}

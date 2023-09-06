using APIDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIDemo.Controllers
{
    [RoutePrefix("Users")]
    public class UsersController : ApiController
    {
        StoreDbContext context= new StoreDbContext();

        [Route("GetAllUsers")]
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return context.Users.ToList();
        }

        [Route("GetUser/{id}")]
        [HttpGet]
        public User GetUserById(string id)
        {
            return context.Users.Find(id);
        }

        [Route("AddUser")]
        [HttpPost]
        public bool InsertUser([FromBody] User user)
        {
            context.Users.Add(user);
            int rowsEffected = context.SaveChanges();

            if(rowsEffected > 0)
                   return true;
            return false;
        }

        [Route("RemoveUser/{id}")]
        [HttpDelete]
        public bool DeleteUser(string id)
        {
            User userObj = context.Users.Find(id);
            int rowsEffected = 0;
            if(userObj != null)
            {
                context.Users.Remove(userObj);
                rowsEffected = context.SaveChanges();
            }
            if (rowsEffected > 0)
                return true;
            return false;
        }

        [Route("UpdateUser/{id}")]
        [HttpPut]
        public bool UpdateUser(string id, [FromBody] User user)
        {
            User userToUpdate = context.Users.Find(id);

            int rowsEffected = 0;

            if(userToUpdate != null)
            {
                userToUpdate.UserName = user.UserName;
                userToUpdate.Password = user.Password;
                rowsEffected = context.SaveChanges();
            }
            if (rowsEffected > 0)
                return true;
            return false;
        }
    }
}

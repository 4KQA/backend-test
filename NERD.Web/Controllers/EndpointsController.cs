using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using NERD.Web.Business.Features;
namespace NERD.Web.Controllers
{

    [RoutePrefix("endpoints")]
    public class EndpointsController : ApiController
    {
        private UserService _userService;
        public EndpointsController()
        {
            _userService = new UserService();
        }

        [HttpGet]
        [Route("getUser")]
        public IHttpActionResult GetUser(int id)

        {
            User user2 = _userService.GetDataFromDB(id);
            return Ok(user2);
        }
        [HttpGet]
        [Route("getAllUsers")]
        public IHttpActionResult GetAllUsers()

        {
            List<User> allUsers = _userService.GetAllUsers();
            return Ok(allUsers);
        }
        [HttpGet]
        [Route("getSurvivors")]
        public IHttpActionResult GetSurvivors()

        {
            List<User> allUsers = _userService.GetSurvivorList();
            return Ok(allUsers);
        }

        [HttpPost]
        [Route("UpdateOrCreate")]
        public IHttpActionResult UpdateOrCreate(User user)
        {

            return Ok(_userService.UpdateOrCreateUser(user));
         
        }
        [HttpGet]
        [Route("getRelatives")]
        public IHttpActionResult FindRelatives(string Lname)
        {
            return Ok(_userService.GetRelatives(Lname));
        
        }
        [HttpGet]
        [Route("getSurvivorPercentage")]
        public IHttpActionResult getSurvivorPercentage()
        {
            List<User> survivors = _userService.GetSurvivorList();
            List<User> allUsers = _userService.GetAllUsers();
            return Ok(_userService.GetSurvivorPercentage(survivors, allUsers));

        }


    }
}
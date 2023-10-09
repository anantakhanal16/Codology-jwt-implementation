using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project__JWT.Models;

namespace Project__JWT.Controllers
{
    public class EmployeeController : Controller
    {

        /// <summary>
        /// yo method yetikai hit garna mildaina 
        /// bearer + token 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("GetData")]
        public string GetData()
        {
            return "Auth with jwt";
        }

    
        [HttpGet]
        [Route("Details")]
        public string Details()
        {
            return "Auth with jwt";
        }

        [Authorize]
        [HttpGet]
        public string AddUser(Users user)
        {
            return "User added with username " + user.UserName;
        }
    }
}

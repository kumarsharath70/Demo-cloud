using ASPCoreWithAngular.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreWithAngular.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly ILogin objlogin;
        
        public LoginController(ILogin _login)
        {
            objlogin = _login;
        }

        [HttpGet]
        [Route("CheckUser")]
        public int CheckUser(string userName, string password)
        {
           return objlogin.CheckUser(userName,password);
        }

        [HttpGet]
        [Route("UpdateReadAction")]
        public int UpdateReadAction(string userName, string actionName, string fileName)
        {
            return objlogin.UpdateReadAction(userName, actionName, fileName);
        }
    }
}

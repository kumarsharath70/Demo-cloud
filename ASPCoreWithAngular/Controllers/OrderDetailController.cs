using ASPCoreWithAngular.Interfaces;
using ASPCoreWithAngular.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreWithAngular.Controllers
{
    [Route("api/[controller]")]
    public class OrderDetailController : Controller
    {
        private readonly IOrderDetail objOrderDetail;

        public OrderDetailController(IOrderDetail _objOrderDetail)
        {
            objOrderDetail = _objOrderDetail;
        }

        [HttpGet]
        [Route("GetOrderDetail")]
        public PODetail GetOrderDetail(string fileName)
        {
            return objOrderDetail.GetPurchaseOrderDetail(fileName);
        }

        [HttpGet]
        [Route("AddMessage")]
        public int AddMessage(string filename, string msg, string userName)
        {
            return objOrderDetail.AddMessage(filename, msg,userName);
        }
    }
}

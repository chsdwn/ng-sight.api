using System;
using System.Linq;
using Advantage.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Advantage.API.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly ApiContext context; 

        public OrderController(ApiContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var orders = context.Orders.Include(c => c.Customer).OrderBy(o => o.Id);

            return Ok(orders);
        }

        [HttpGet("{pageIndex:int}/{pageSize:int}")]
        public IActionResult Get(int pageIndex, int pageSize)
        {
            var orders = context.Orders.Include(o => o.Customer).OrderByDescending(c => c.Placed);
            var page = new PaginatedResponse<Order>(orders, pageIndex, pageSize);
            var totalCount = orders.Count();
            var totalPages = Math.Ceiling((double)totalCount / pageSize);

            var response = new 
            {
                Page = page,
                TotalPages = totalPages
            };

            return Ok(response);
        }
    }
}
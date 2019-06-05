using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Phonebook.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly Services.ICustomerService service;

        public CustomerController(Services.ICustomerService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IEnumerable<Customers.Customer> Get()
        {
            return service.All();
        }

        [HttpGet("{id}")]
        public IEnumerable<Customers.Number> Get(int id)
        {
            return service.Get(id);
        }
        
        [HttpPut("{customer_id}/{number_id}")]
        public bool Activate(int customer_id, int number_id)
        {
            return service.Activate(customer_id, number_id);
        }
    };
};
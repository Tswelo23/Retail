using Azure.Data.Tables;
using Microsoft.AspNetCore.Mvc;
using Retail.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Retail.Controllers
{
    public class CustomerController : Controller
    {
        private readonly TableServiceClient _tableServiceClient;

        public CustomerController(TableServiceClient tableServiceClient)
        {
            _tableServiceClient = tableServiceClient;
        }

            public async Task<IActionResult> Index()
            {
                var tableClient = _tableServiceClient.GetTableClient("CustomerProfiles");

                var entities = new List<CustomerProfile>();
                await foreach (var entity in tableClient.QueryAsync<CustomerProfile>())
                {
                    entities.Add(entity);
                }

                return View(entities);
            }

            // GET: /Customer/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: /Customer/Create
            [HttpPost]
            public async Task<IActionResult> Create(CustomerProfile customer)
            {
                if (!ModelState.IsValid)
                {
                    return View(customer);
                }

                var tableClient = _tableServiceClient.GetTableClient("CustomerProfiles");

                try
                {
                    await tableClient.AddEntityAsync(customer);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error creating customer: {ex.Message}");
                    return View(customer);
                }

                return RedirectToAction("Index");
            }
        }

    }

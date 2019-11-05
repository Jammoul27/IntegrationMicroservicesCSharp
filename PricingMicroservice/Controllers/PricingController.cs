using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using RabbitMQ.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PricingMicroservice.Models;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace PricingMicroservice.Controllers
{
    [ApiController]
    [Route("api/pricing")]
    public class PricingController : ControllerBase
    {

        private async Task<InsurancePolicy> GetPolicy(int id)
        {

            InsurancePolicy policy;

            //We will make a GET request to a really cool website...

            string baseUrl = "http://localhost:1434/api/policy/" + id;
            //The 'using' will help to prevent memory leaks.
            //Create a new instance of HttpClient
            using (HttpClient client = new HttpClient())

            //Setting up the response...         

            using (HttpResponseMessage res = await client.GetAsync(baseUrl))
            using (HttpContent content = res.Content)
            {
                string data = await content.ReadAsStringAsync();
                if (data != null)
                {
                    if (res.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        policy = JsonSerializer.Deserialize<InsurancePolicy>(data, options);
                        return policy;
                    }
                }
                else
                {
                    throw (new Exception("null"));
                }
                return null;
            }
        }

        private async Task<double> CalculateFinalPrice(int id, int age, int supported, int salary)
        {
            double price = 0;
            InsurancePolicy policy = await GetPolicy(id);

            if (policy == null)
            {
                throw (new Exception("Null Policy"));
            }

            price += policy.BasePrice;

            //age logic

            if (age > 30)
            {
                price += 30000;
                if (age > 64)
                {
                    price += 15000;
                }
            }

            //salary logic

            if (salary > 1000000)
            {
                price += 5000;
                if (salary > 1500000)
                {
                    price += 3000;
                }
            }

            //number of supported members logic

            if (policy.BasePrice > 80000)
            {
                if (supported > 9)
                {
                    price += 10000;
                }
            }
            else if (policy.BasePrice > 92000) { 
                if (supported > 6)
                {
                    price += 11000;
                }
            }
            else
            {
                if (supported > 3)
                {
                    price += 12000;
                }
            }

            return price;
        }

        private void publishMessage(string message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "policyOrder",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "policyOrder",
                                     basicProperties: null,
                                     body: body);
            }
        }


        [HttpGet("{id:int}/{age:int}/{supported:int}/{salary:int}")]
        public async Task<IActionResult> Get(int id, int age, int supported, int salary)
        {
            if (id == null || age == null || supported == null || salary == null)
            {
                return BadRequest("Missing GET request parameter(s)\n" +
                    "Parametes : id,age,# of supported members,salary");
            }
            double price = await CalculateFinalPrice(id, age, supported, salary);

            //price += get request answer for base price;
            return Ok("Your calculated price is : " + price + " L.L.");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClientInfo info)
        {
            if (info.PolicyId == null || info.age == null || info.supported == null || info.salary == null || info.email == null)
            {
                return BadRequest("Missing GET request parameter(s)\n" +
                    "Parametes : id,age,# of supported members,salary, email");
            }

            InsurancePolicy policy = await GetPolicy(info.PolicyId);
            Console.WriteLine("policy get ok... \n");

            double price = await CalculateFinalPrice(info.PolicyId, info.age, info.supported, info.salary);

            Console.WriteLine("final price get ok... \n");


            Order order = new Order();
            order.email = info.email;
            order.policy = policy;
            order.finalPrice = price;

            Console.WriteLine("order create ok... \n");


            string orderString;

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            orderString = JsonSerializer.Serialize<Order>(order, options);

            Console.WriteLine("json string create ok... \n");


            //get request by ID on catalog to get name and description;

            //publish rabbitmq message with name,description, email, and final price

            publishMessage(orderString);

            Console.WriteLine("message publish ok... \n");


            return Ok("Order Received!");
        }
    }
}

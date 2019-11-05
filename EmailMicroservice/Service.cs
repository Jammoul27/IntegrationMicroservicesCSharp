using EmailMicroservice.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace EmailMicroservice
{
    class Service
    {
        static void Main(string[] args)
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

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Order order = JsonSerializer.Deserialize<Order>(message, options);

                    Console.WriteLine("order deserialize ok... \n");

                    try
                    {
                        SmtpClient mySmtpClient = new SmtpClient("smtp.gmail.com");

                        mySmtpClient.EnableSsl = true;

                        // set smtp-client with basicAuthentication
                        mySmtpClient.UseDefaultCredentials = false;
                        System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential("japolicies@gmail.com", "HelloWorld1234!");
                        mySmtpClient.Credentials = basicAuthenticationInfo;

                        Console.WriteLine("smtp authentication set... \n");


                        // add from,to mailaddresses
                        MailAddress from = new MailAddress("no-reply@japolicy.com", "JA Policy Center");
                        MailAddress to = new MailAddress(order.email);
                        MailMessage myMail = new System.Net.Mail.MailMessage(from, to);


                        // set subject and encoding
                        myMail.Subject = "Thank you for your order!";
                        myMail.SubjectEncoding = System.Text.Encoding.UTF8;

                        // set body-message and encoding
                        myMail.Body = "<b>Order Received : </b><br>Client email : <b>" + order.email + 
                                                           "</b><br>Policy Name : <b> " + order.policy.Name + 
                                                           "</b><br> Policy Description : <b> " + order.policy.Description + 
                                                           "</b><br> Price <b> : "+order.finalPrice+ "L.L per month</b>";
                        myMail.BodyEncoding = System.Text.Encoding.UTF8;
                        // text or html
                        myMail.IsBodyHtml = true;

                        mySmtpClient.Send(myMail);
                        Console.WriteLine("email sent ok... \n");
                    }
                    catch (SmtpException ex)
                    {
                        throw new ApplicationException
                              ("SmtpException has occured: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }; 
                Console.WriteLine("listening to queue ... \n");
                channel.BasicConsume(queue: "policyOrder",
                                 autoAck: true,
                                 consumer: consumer);
                Console.ReadLine();
            }
        }
    }
}





using Microsoft.AspNetCore.Mvc;
using Grpc.Net.Client;

namespace GrpcGreetAPI.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class GRPCEndPointController : ControllerBase
    {
        
        [HttpPost(Name = "PostGrpc")]
        public async Task<IActionResult> PostSomething(Person person)
        {
            string name = $"{person.Firstname} {person.LastName}";
            string address = $"https://localhost:7246";

            using var channel = GrpcChannel.ForAddress(address);

            var client = new Greeter.GreeterClient(channel);

            var reply = await client.SayHelloAsync(
                              new HelloRequest { Name = name, Age = person.Age });

            return Ok(reply);
        }
    }

}
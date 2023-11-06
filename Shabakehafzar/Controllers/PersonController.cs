using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shabakehafzar.API.Base;
using Shabakehafzar.Application.DTOs.RequestModels.Person;
using Shabakehafzar.Application.Service.Persons;

namespace Shabakehafzar.API.Controllers
{
    [Route("api/Shabakehafzar/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonController : ApiControllerBase
    {
        private readonly IPersonService _personService;
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("Linq")]
        public async Task<IActionResult> GetWithLinq()
        {
            var result = await _personService.GetAllWithLinq();
            return OkResult("All Person With Linq", result, result.Count());
        }

        [HttpGet("Lambda")]
        public async Task<IActionResult> GetWithLambda()
        {
            var result = await _personService.GetAllWithLambda();
            return OkResult("All Person With Linq", result, result.Count());
        }

        [HttpGet("TSQL")]
        public async Task<IActionResult> GetWithTSQL()
        {
            var result = await _personService.GetAllWithLambda();
            return OkResult("All Person With Linq", result, result.Count());
        }


        [HttpPost()]
        public async Task<IActionResult> Post(CreatePersonRequest command)
        {
            var result = await _personService.Create(command);
            return OkResult("person Created.",result);
        }
    }
}

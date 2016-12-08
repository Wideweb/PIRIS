using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Common.Core.ActionFilters;
using Identity.Core.Features.User.Services.Interfaces;
using Identity.Core.Features.Questionnaire.Services;
using Identity.Core.Features.User.Models;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    public class ClientController : Controller
    {
        private readonly IClientService clientService;

        public ClientController(IClientService clientService)
        {
            this.clientService = clientService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await clientService.GetClientById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClientModel clientModel)
        {
            return Ok(await clientService.SaveClient(clientModel));
        }

        [HttpGet("GetClientList")]
        [PaginationFilter]
        public async Task<IActionResult> Get()
        {
            return Ok(await clientService.GetClientList());
        }

        [HttpGet("GetClientForm")]
        public async Task<IActionResult> GetClientForm()
        {
            return Ok(await Task.FromResult(new FormService().GetForm(typeof(ClientModel))));
        }
    }
}

namespace CmdApi.Controllers
{
    using CmdApi.Services;
    using CmdApi.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Runtime.CompilerServices;

    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {

        private readonly ICommandServices commandServices;

        public CommandsController(ICommandServices _commandServices)
        {
            commandServices = _commandServices;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll()
            => Ok(await commandServices.GetAllCommandsAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommandById([FromRoute] int id)
            => Ok(await commandServices.GetCommandAsync(id));

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] CommandViewModel commandViewModel)
        {
            var commandId = await commandServices.GreateCommandAsync(commandViewModel);

            var command = await commandServices.GetCommandAsync(commandId);

            return CreatedAtAction(nameof(GetCommandById), new CommandViewModel { Id = commandId }, command);
        }

        [HttpPost]
        [Route("CreateForm")]
        public async Task<IActionResult> CreateForm([FromForm] CommandViewModel commandViewModel)
        {
            var commandId = await commandServices.GreateCommandAsync(commandViewModel);

            var command = await commandServices.GetCommandAsync(commandId);

            return CreatedAtAction(nameof(GetCommandById), new CommandViewModel { Id = commandId }, command);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CommandViewModel commandViewModel)
        {

            var model = await commandServices.UpdateAsync(id, commandViewModel);

            return Ok(model);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            
            var model = await commandServices.DeleteAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);

        }
    }
}

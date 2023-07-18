﻿namespace CmdApi.Controllers
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

            return CreatedAtAction(nameof(GetCommandById), new CommandViewModel { Id = commandId}, commandViewModel);
        }
    }
}

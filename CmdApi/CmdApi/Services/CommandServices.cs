namespace CmdApi.Services
{
    using CmdApi.Data;
    using CmdApi.Models;
    using CmdApi.ViewModels;
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading.Tasks;

    public class CommandServices : ICommandServices
    {
        private CmdApiDbContext dbContext;

        public CommandServices(CmdApiDbContext _dbContext)
        {
            dbContext = _dbContext;
        }


        public async Task<ICollection<CommandViewModel>> GetAllCommandsAsync()
            => await dbContext.Commands.Select(c => new CommandViewModel
            {
                Id = c.Id,
                HowTo = c.HowTo,
                CommandLine = c.CommandLine,
                Platform = c.Platform
            }).ToListAsync();


        public async Task<CommandViewModel> GetCommandAsync(int Id)
            => await dbContext
                .Commands
                .Where(c => c.Id.Equals(Id))
                .Select(c => new CommandViewModel
                {
                    Id = c.Id,
                    HowTo = c.HowTo,
                    CommandLine = c.CommandLine,
                    Platform = c.Platform
                }).FirstAsync();

        public async Task<int> GreateCommandAsync(CommandViewModel model)
        {
            var command = new Command
            {
                HowTo = model.HowTo,
                CommandLine = model.CommandLine,
                Platform = model.Platform
            };

            await dbContext.Commands.AddAsync(command);

            await dbContext.SaveChangesAsync();

            return command.Id;
        }
    }
}

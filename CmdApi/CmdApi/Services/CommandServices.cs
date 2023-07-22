namespace CmdApi.Services
{
    using CmdApi.Data;
    using CmdApi.Models;
    using CmdApi.ViewModels;
    using Microsoft.EntityFrameworkCore;
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

        public async Task<CommandViewModel> UpdateAsync(int id, CommandViewModel commandViewModel)
        {

            var command = new Command
            {
                Id = commandViewModel.Id == 0 ? id : commandViewModel.Id,
                Platform = commandViewModel.Platform,
                CommandLine = commandViewModel.CommandLine,
                HowTo = commandViewModel.HowTo
            };

            dbContext.Entry(command).State = EntityState.Modified;


            dbContext.Update(command);
            await dbContext.SaveChangesAsync();

            var commandFromDB = await GetCommandAsync(command.Id);

            return commandFromDB;
        }

        public async Task<CommandViewModel> DeleteAsync(int id)
        {

            var entity = dbContext.Commands.Find(id);

            if (entity == null)
            {
                return null;
            }

            var commandFromDB = await GetCommandAsync(id);

            dbContext.Commands.Remove(entity);
            await dbContext.SaveChangesAsync();

            return commandFromDB;
        }
    }
}

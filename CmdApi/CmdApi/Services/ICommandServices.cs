namespace CmdApi.Services
{
    using CmdApi.ViewModels;

    public interface ICommandServices
    {
        Task<ICollection<CommandViewModel>> GetAllCommandsAsync();

        Task<CommandViewModel> GetCommandAsync(int Id);

        Task<int> GreateCommandAsync(CommandViewModel model);
    }
}

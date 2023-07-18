namespace CmdApi.ViewModels
{
    public class CommandViewModel
    {
        public int Id { get; set; }

        public string HowTo { get; set; } = null!;

        public string Platform { get; set; } = null!;

        public string CommandLine { get; set; } = null!;
    }
}

namespace CmdApi.Models
{
    public class Command
    {
        public int Id { get; set; }

        public string HowTo { get; set; } = null!;

        public string Platform { get; set; } = null!;

        public string CommandLine { get; set; } = null!;
    }
}

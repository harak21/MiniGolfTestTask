namespace MiniGolf.Utility.Logging
{
    public static class Log
    {
        public static readonly TagLog Default = new(string.Empty);
        public static readonly TagLog Loading = new("LOADING");
        public static readonly TagLog Boot = new("BOOT");
    }
}
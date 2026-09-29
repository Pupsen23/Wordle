namespace Wordle.Cli
{
    public class ArgumentParser
    {
        public const string defaultArgName = "--default";
        public const string seedArgName = "--seed";
        public const string maxAttemptsArgName = "--max-attempts";
        public const string correctWordArgName = "--correct-word";
        public static Core.Config GetConfig(string[] args)
        {   
            if (args.Contains(defaultArgName))
                return new Core.Config();

            string tempArgValue;
            int seedArgValue;
            int maxAttemptsArgValue;
            string correctWordArgValue;

            {
                tempArgValue = FindArgumentValue(args, seedArgName);
                ConvertArgumentValue(tempArgValue, out seedArgValue);
            }
            {
                tempArgValue = FindArgumentValue(args, maxAttemptsArgName);
                ConvertArgumentValue(tempArgValue, out maxAttemptsArgValue);
            }
            {
                tempArgValue = FindArgumentValue(args, correctWordArgName);
                ConvertArgumentValue(tempArgValue, out correctWordArgValue );
            }

            return new Core.Config(seedArgValue, maxAttemptsArgValue, correctWordArgValue);
        }
        private static string FindArgumentValue(string[] args, string argName)
        {
            int argNameIndex = args.IndexOf(argName);

            if (argNameIndex == -1)
                throw new ArgumentException($"Cannot find argument name '{argName}' in given arguments.");

            int argValueIndex = argNameIndex + 1;

            if (argValueIndex >= args.Length)
                throw new ArgumentException($"Cannot find argument value for argument name '{argName}' in given arguments.");

            return args[argValueIndex];
        }
        private static void ConvertArgumentValue(string? argValue, out int argValueConverted)
        {
            if (!int.TryParse(argValue, out argValueConverted))
                throw new ArgumentException($"Failed to convert '{argValue}' to integer.");
        }
        private static void ConvertArgumentValue(string? argValue, out string argValueConverted)
        {
            if (string.IsNullOrEmpty(argValue))
                throw new ArgumentNullException($"Failed to convert '{argValue}' to string.");

            argValueConverted = argValue;
        }
    }
}
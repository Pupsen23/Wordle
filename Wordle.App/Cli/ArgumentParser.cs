using Wordle.Core;

namespace Wordle.Cli
{
    public class ArgumentParser
    {
        public const string defaultArgName = "--default";
        public const string maxAttemptsArgName = "--max-attempts";
        public const string seedArgName = "--seed";
        public const string correctWordArgName = "--correct-word";
        public static Config GetConfig(string[] args)
        {
            if (args.Contains(defaultArgName))
                return new Config();

            string tempArgValue;
            int seedArgValue;
            int maxAttemptsArgValue;
            string correctWordArgValue;
            ArgumentException seedException;
            ArgumentException correctWordException;

            tempArgValue = FindArgumentValue(args, maxAttemptsArgName);
            ConvertArgumentValue(tempArgValue, out maxAttemptsArgValue);

            try
            {
                tempArgValue = FindArgumentValue(args, seedArgName);
                ConvertArgumentValue(tempArgValue, out seedArgValue);
                return new Config(maxAttemptsArgValue, seedArgValue);
            }
            catch(ArgumentException ex) { seedException = ex; }

            try
            {
                tempArgValue = FindArgumentValue(args, correctWordArgName);
                ConvertArgumentValue(tempArgValue, out correctWordArgValue );
                return new Config(maxAttemptsArgValue, correctWordArgValue);
            }
            catch (ArgumentException ex) { correctWordException = ex; }

            throw new ArgumentException(seedException.Message + ' ' + correctWordException.Message);
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

            if (!WordNormalizer.CheckWordSymbols(argValue))
                throw new ArgumentException($"Forbidden symbol(s) found in '--correct-word' argument value: {argValue}.");

            argValueConverted = argValue.Trim().ToLowerInvariant();
        }
    }
}
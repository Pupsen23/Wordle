using Wordle.Cli.StringCollection;
using Wordle.Core;
using Wordle.Core.Words;

namespace Wordle.Cli
{
    public class ArgumentParser
    {
        public static Config GetConfig(string[] args, out List<ArgumentException> argumentParseExceptions)
        {
            argumentParseExceptions = [];
            Config config = new Config()
            {
                ShowArgumentParseExceptions = args.Contains(Arguments.ShowArgumentParseExceptions),
                ShowHelp = args.Contains(Arguments.ShowHelp)
            };

            try
            {
                string argValue = FindArgumentValue(args, Arguments.MaxAttempts);
                ConvertArgumentValue(argValue, out int convertedArgValue);
                config.MaxAttempts = convertedArgValue;
            }
            catch (ArgumentException ex) { argumentParseExceptions.Add(ex); }

            try
            {
                string argValue = FindArgumentValue(args, Arguments.Seed);
                ConvertArgumentValue(argValue, out int convertedArgValue);
                config.Seed = convertedArgValue;
            }
            catch (ArgumentException ex) { argumentParseExceptions.Add(ex); }

            try
            {
                string argValue = FindArgumentValue(args, Arguments.Word);
                ConvertArgumentValue(argValue, out SecretWord convertedArgValue);
                config.Word = convertedArgValue;
            }
            catch (ArgumentException ex) { argumentParseExceptions.Add(ex); }

            return config;
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
        /// <exception cref="ArgumentException"></exception>
        private static void ConvertArgumentValue(string? argValue, out int argValueConverted)
        {
            if (!int.TryParse(argValue, out argValueConverted))
                throw new ArgumentException($"Failed to convert argument value '{argValue}' to integer.");
        }
        /// <exception cref="ArgumentException"></exception>
        private static void ConvertArgumentValue(string? argValue, out SecretWord argValueConverted)
        {
            argValueConverted = new SecretWord(argValue);
        }
        /// <exception cref="ArgumentException"></exception>
        private static void ConvertArgumentValue(string? argValue, out Word argValueConverted)
        {
            argValueConverted = new Word(argValue);
        }
    }
}
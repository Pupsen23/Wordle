using Wordle.Core;
using Wordle.Core.Words;

namespace Wordle.Cli
{
    public class ArgumentParser
    {
        public const string DeterminedArgName = "-d";
        public const string ShowArgumentParseExceptionsArgName = "-s";
        public const string ShowHelpArgName = "--help";
        public const string MaxAttemptsArgName = "--max-attempts";
        public const string SeedArgName = "--seed";
        public const string WordArgName = "--word";
        public const string InstantGuessWord = "--instant-guess-word";
        public static Config GetConfig(string[] args, out List<ArgumentException> argumentParseExceptions)
        {
            argumentParseExceptions = [];
            Config config = new Config()
            {
                IsDetermined = args.Contains(DeterminedArgName),
                ShowArgumentParseExceptions = args.Contains(ShowArgumentParseExceptionsArgName),
                ShowHelp = args.Contains(ShowHelpArgName)
            };

            try
            {
                string argValue = FindArgumentValue(args, MaxAttemptsArgName);
                ConvertArgumentValue(argValue, out int convertedArgValue);
                config.MaxAttempts = convertedArgValue;
            }
            catch (ArgumentException ex) { argumentParseExceptions.Add(ex); }

            try
            {
                string argValue = FindArgumentValue(args, SeedArgName);
                ConvertArgumentValue(argValue, out int convertedArgValue);
                config.Seed = convertedArgValue;
            }
            catch (ArgumentException ex) { argumentParseExceptions.Add(ex); }

            try
            {
                string argValue = FindArgumentValue(args, WordArgName);
                ConvertArgumentValue(argValue, out SecretWord convertedArgValue);
                config.Word = convertedArgValue;
            }
            catch (ArgumentException ex) { argumentParseExceptions.Add(ex); }

            try
            {
                string argValue = FindArgumentValue(args, InstantGuessWord);
                ConvertArgumentValue(argValue, out Word convertedArgValue);
                config.InstantGuessWord = convertedArgValue;
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
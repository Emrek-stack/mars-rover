using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Mars.Rover.Core.Domain;
using Mars.Rover.Core.Domain.Impl;

namespace Mars.Rover.Core.Core
{
	public class CommandParser : ICommandParser
	{
		public bool IsValid (string[] commands)
		{
			return !IsNotValid (commands);
		}

		public bool IsNotValid (string[] commands)
		{
			if (commands ==  null) 
				throw new ArgumentNullException (nameof(commands));

			foreach (var t in commands)
            {
                var hasOnlyValidChars =  Regex.IsMatch(t, @"^[LRMNSEW0123456789\s]");
                if (!hasOnlyValidChars) return true;
            }

			return false;
		}

		public string[] SplitCommandString (string command)
		{
			if (string.IsNullOrWhiteSpace (command)) 
				throw new ArgumentNullException (nameof(command));

			return command.Split (new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
		}

		string[] ValidateCommand (string command)
		{
			if (string.IsNullOrWhiteSpace (command))
				throw new ArgumentNullException (nameof(command));
			var commands = SplitCommandString (command);
			if (IsNotValid (commands))
				throw new ArgumentException ("command is not valid");
			return commands;
		}

		public List<IResearchInfo> GetResearchInfos (string command)
		{
			var commands = ValidateCommand (command);

			var result = new List<IResearchInfo> ();
			for (int i = 1; i < commands.Length; i = i + 2) {

				var positionString = commands[i];
				var explorationString = commands[i+1];

				if (string.IsNullOrWhiteSpace (positionString) 
				    || string.IsNullOrWhiteSpace (explorationString))
					throw new ArgumentException ("command has empty line > " + i);

				result.Add (new ResearchInfo { RoverPosition = positionString.Trim(), 
											   RoverExploration = explorationString.Trim() });
			}

			return result;
		}

		public List<int> GetPlateauMetrics (string command)
		{
			var commands = ValidateCommand (command);

			var items = commands [0].Split(new[]{' '}, StringSplitOptions.RemoveEmptyEntries);

            var result = new List<int> {Convert.ToInt32(items[0]), Convert.ToInt32(items[1])};

            return result;
		}
	}
}

using System.Collections.Generic;

namespace Mars.Rover.Core.Domain
{
	public interface ICommandParser
	{
		bool IsValid (string[] commands);
		bool IsNotValid (string[] commands);
		string[] SplitCommandString (string command);
		List<IResearchInfo> GetResearchInfos (string command);
		List<int> GetPlateauMetrics (string command);
	}
}
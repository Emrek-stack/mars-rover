using System.Collections.Generic;

namespace Mars.Rover.Core.Domain
{
	public interface ISpaceStation
	{
		IPlanet Mars { get; }
		Queue<string> UnprocessedCommands { get; }
		Queue<IResearchInfo> ResearchInfos {	get; }

		void ValidateCommandsAndEnqueueResearchInfos ();
		IPlateau DefinePlateau (int width, int height);
		bool StartResearching ();
	}
}
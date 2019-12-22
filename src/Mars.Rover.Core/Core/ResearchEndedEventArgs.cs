using System;

namespace Mars.Rover.Core.Core
{
	public class ResearchEndedEventArgs : EventArgs
	{
        public string Position { get; }

        public ResearchEndedEventArgs(string position)
		{
			Position = position;
		}
	}
}
using System.Collections.Generic;
using System.Drawing;

namespace Mars.Rover.Core.Domain
{
	public interface ISpaceAgency
	{
		Dictionary<string, Bitmap> Photos { get; }

		IRover CreateRover ();
		ISpaceStation CreateSpaceStation ();
		void SendRoverToMars (IPlanet mars, IRover rover);
		void SendCommandsToSpaceStation (ISpaceStation spaceStation, string commands);
	}
}
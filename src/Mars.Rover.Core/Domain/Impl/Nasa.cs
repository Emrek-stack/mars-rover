using System;
using System.Collections.Generic;
using System.Drawing;
using Mars.Rover.Core.Core;

namespace Mars.Rover.Core.Domain.Impl
{
	public class Nasa : ISpaceAgency
	{
        public Dictionary<string, Bitmap> Photos { get; }

        public Nasa ()
		{
			Photos = new Dictionary<string, Bitmap> ();
		}

		public IRover CreateRover ()
		{
			return new Rover(this, new Camera());
		}

		public ISpaceStation CreateSpaceStation ()
		{
			return new SpaceStation (new Mars(), new CommandParser());
		}

		public void SendRoverToMars (IPlanet mars, IRover rover)
		{
			if (mars == null)
				throw new ArgumentNullException(nameof(mars));

			if (rover == null)
				throw new ArgumentNullException(nameof(rover));

			mars.Rovers.Add (rover);
		}

		public void SendCommandsToSpaceStation (ISpaceStation spaceStation, string commands)
		{
			if (spaceStation == null)
				throw new ArgumentNullException(nameof(spaceStation));

			if (string.IsNullOrWhiteSpace(commands))
				throw new ArgumentNullException(nameof(commands));

			spaceStation.UnprocessedCommands.Enqueue (commands);
			spaceStation.ValidateCommandsAndEnqueueResearchInfos ();
		}
	}
}
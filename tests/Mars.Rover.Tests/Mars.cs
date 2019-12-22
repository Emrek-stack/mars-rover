using System.Collections.Generic;
using Mars.Rover.Core.Domain;

namespace Mars.Rover.Tests
{
	public class Mars : IPlanet
	{
		public IPlateau Plateau {
			get;
			set;
		}

        public List<IRover> Rovers { get; }

        public Mars ()
		{
			Rovers = new List<IRover> ();
		}
	}
}


using System.Collections.Generic;

namespace Mars.Rover.Core.Domain.Impl
{
	public class Mars : IPlanet
	{
		public IPlateau Plateau { get; set;	}

        public List<IRover> Rovers { get; }

        public Mars ()
		{
			Rovers = new List<IRover> ();
		}
	}
}
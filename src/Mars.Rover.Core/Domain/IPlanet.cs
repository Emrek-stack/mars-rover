using System.Collections.Generic;

namespace Mars.Rover.Core.Domain
{
	public interface IPlanet
	{
		IPlateau Plateau { get; set;	}
		List<IRover> Rovers { get;	}
	}
}
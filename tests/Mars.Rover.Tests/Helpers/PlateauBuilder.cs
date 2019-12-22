using Mars.Rover.Core.Domain;
using Mars.Rover.Core.Domain.Impl;

namespace Mars.Rover.Tests.Helpers
{
	public class PlateauBuilder
	{
        internal IPlateau Build()
		{
			return new Plateau(5,5);
		}
	}
}
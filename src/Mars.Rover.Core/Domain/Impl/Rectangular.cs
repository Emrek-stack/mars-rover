namespace Mars.Rover.Core.Domain.Impl
{
	public class Rectangular : IRectangular
	{
		public int Width { get; set; }
		public int Height { get; set;}

		public int Size => Width * Height;
    }
}
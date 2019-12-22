using System.Drawing;

namespace Mars.Rover.Core.Domain.Impl
{
	public class Camera : ICamera
	{
		public Bitmap TakePhoto ()
		{
			return new Bitmap(100, 100);
		}
	}
}
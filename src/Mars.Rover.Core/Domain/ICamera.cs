using System.Drawing;

namespace Mars.Rover.Core.Domain
{
	public interface ICamera
	{
		Bitmap TakePhoto ();
	}
}
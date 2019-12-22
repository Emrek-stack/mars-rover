using System;
using System.Collections.Generic;
using System.Drawing;

namespace Mars.Rover.Core.Domain
{
	public delegate void ResearchEndedEventHandler (object sender, EventArgs e);

	public interface IRover
	{
		event ResearchEndedEventHandler ResearchEnded;

		string Name { get;}
		bool IsResearching {	get; set; }
		ICamera Camera {get;}
		Dictionary<string,Bitmap> Photos { get; }
		CompassPoint Position{ get;	set; }
		ILocation Location {	get; set; }

		void TakePhoto ();
		void SendPhotosToNasa ();

		bool Spin (char spiningSide);
		bool MoveForward ();

		void Research (IResearchInfo researchInfo);
	}
}


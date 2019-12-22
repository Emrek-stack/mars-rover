using System;
using System.Collections.Generic;
using System.Drawing;
using Mars.Rover.Core.Core;

namespace Mars.Rover.Core.Domain.Impl
{
	public class Rover : IRover
	{
		public event ResearchEndedEventHandler ResearchEnded;

        public string Name { get; }

        public bool IsResearching { get; set; }
		public CompassPoint Position { get; set; }
		public ILocation Location { get;	set; }

        public ICamera Camera { get; }

        public Dictionary<string,Bitmap> Photos { get; }

        private readonly ISpaceAgency _nasa;

		public Rover (ISpaceAgency nasa, ICamera camera)
		{
			_nasa = nasa;
			Camera = camera;
			Name = Guid.NewGuid ().ToString ();
			Photos = new Dictionary<string,Bitmap> ();

			Location = new Location { X = 0, Y = 0 };
		}

		public void TakePhoto ()
		{
			var image = Camera.TakePhoto ();
			var imagaName = $"{Name}-{Guid.NewGuid().ToString()}.bmp";
			Photos.Add (imagaName, image);
		}

		public void SendPhotosToNasa ()
		{
			foreach (var item in Photos) {
				_nasa.Photos.Add (item.Key, item.Value);
			}		

			Photos.Clear ();
		}

		public bool Spin (char spiningSide)
		{
			var positionChar = Position.ToString () [0];
			switch (spiningSide) {
				case 'L':
				SpinLeft (positionChar);
					break;
				case 'R':
				SpinRight (positionChar);
					break;
				default:
					throw new ArgumentException ("not valid spiningSide");					
			}

			return true;
		}

		private void SpinLeft (char direction)
		{
			var lookupTable = new Dictionary<char, CompassPoint> {
				{'N', CompassPoint.West},
				{'W', CompassPoint.South},
				{'S', CompassPoint.East},
				{'E', CompassPoint.North}
			};
			Position = lookupTable [direction];
		}

		private void SpinRight (char direction)
		{
			var lookupTable = new Dictionary<char, CompassPoint> {
				{'N', CompassPoint.East},
				{'W', CompassPoint.North},
				{'S', CompassPoint.West},
				{'E', CompassPoint.South}
			};
			Position = lookupTable [direction];
		}

		public bool MoveForward ()
		{
			if (Position == CompassPoint.North) {
				Location.Y++;
			} 
			else if (Position == CompassPoint.South) {
				Location.Y--;
			}
			else if (Position == CompassPoint.East) {
				Location.X++;
			}
			else if (Position == CompassPoint.West) {
				Location.X--;
			}

			return true;
		}

		public void Research (IResearchInfo researchInfo)
		{
			GetSet (researchInfo.RoverPosition);
			ProcessCommands (researchInfo.RoverExploration);

			Camera.TakePhoto ();

			var exploreEndedEventArgs = new ResearchEndedEventArgs (
                $"{Location.X} {Location.Y} {Position.ToString()[0]}");
			ResearchEnded(this, exploreEndedEventArgs);
		}

		void ProcessCommands (string commands)
		{
			foreach (char command in commands) {
				if (command == 'M') {
					MoveForward ();
				} else {
					Spin (command);
				}
			}
		}

		private void GetSet (string roverPosition)
		{
			var items = roverPosition.Split(new[]{' '}, StringSplitOptions.RemoveEmptyEntries);

			var x = Convert.ToInt32 (items[0]);
			var y = Convert.ToInt32 (items[1]);

			Location = new Location { X = x, Y = y };
			SetPosition (items [2][0]);
		}

		private void SetPosition (char direction)
		{
			var lookupTable = new Dictionary<char, CompassPoint> {
				{'N', CompassPoint.North},
				{'W', CompassPoint.West},
				{'S', CompassPoint.South},
				{'E', CompassPoint.East}
			};
			Position = lookupTable [direction];
		}
	}

}


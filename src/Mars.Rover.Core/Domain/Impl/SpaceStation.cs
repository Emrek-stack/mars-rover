using System;
using System.Collections.Generic;
using System.Linq;
using Mars.Rover.Core.Core;

namespace Mars.Rover.Core.Domain.Impl
{
	public class SpaceStation : ISpaceStation
	{
        public Queue<string> UnprocessedCommands { get; }

        public Queue<IResearchInfo> ResearchInfos { get; }

        public IPlanet Mars { get; }

        private readonly ICommandParser _commandParser;

        public SpaceStation (IPlanet mars, ICommandParser commandParser)
		{
			Mars = mars;
			_commandParser = commandParser;

			UnprocessedCommands = new Queue<string> ();
			ResearchInfos = new Queue<IResearchInfo> ();
		}

		public void ValidateCommandsAndEnqueueResearchInfos ()
		{
			while (UnprocessedCommands.Count > 0) {
				var command = UnprocessedCommands.Dequeue ();

				if (Mars == null) {
					throw new Exception ("space station should connect to mars");
				}

				if (Mars.Plateau == null) {
					var plateauMetrics = _commandParser.GetPlateauMetrics (command);
					Mars.Plateau = DefinePlateau (plateauMetrics [0], plateauMetrics [1]);
				}

				var researchInfos = _commandParser.GetResearchInfos (command);
				foreach (var info in researchInfos) {
					ResearchInfos.Enqueue (info);
				}
			}
		}

		public IPlateau DefinePlateau (int width, int height)
		{
			return new Plateau (width, height);
		}

		public bool StartResearching ()
		{
			if (ResearchInfos.Count == 0)
				throw new Exception ("there is no research info to run rovers");

			DoNextResearch ();

			return true;
		}

		void RoverResearchEnded (object sender, EventArgs e)
		{
			var args = (ResearchEndedEventArgs)e;
			Console.WriteLine ("rover research ended > " + args.Position);
			DoNextResearch ();
		}

		private void DoNextResearch ()
		{
			if (ResearchInfos.Count == 0) {
				Console.WriteLine ("all researches completed...");
				return;
			}

			var researchInfo = ResearchInfos.Dequeue ();
			var rover = ConnectToRover ();
			rover.ResearchEnded += RoverResearchEnded;
			rover.Research (researchInfo);
		}

		IRover ConnectToRover ()
		{
			if (Mars.Rovers.Count == 0)
				throw new Exception ("there is no rover on mars");

			var rover = Mars.Rovers.FirstOrDefault (x => !x.IsResearching);
			if (rover == null)
				throw new Exception ("all rovers are busy please try again later");

			rover.IsResearching = true;
			return rover;
		}
	}
}
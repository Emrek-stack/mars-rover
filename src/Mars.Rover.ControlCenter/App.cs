using Mars.Rover.Core.Domain;

namespace Mars.Rover.ControlCenter
{
    internal class App
    {
        const string NavigationCommand = @"5 5
											1 2 N
											LMLMLMLMM
											3 3 E
											MMRMMRMRRM";

        private readonly ISpaceAgency _spaceAgency;

        public App(ISpaceAgency spaceAgency)
        {
            _spaceAgency = spaceAgency;
        }

        public void Init()
        {
            var station = _spaceAgency.CreateSpaceStation();

            for (int i = 0; i < 3; i++)
            {
                _spaceAgency.SendRoverToMars(station.Mars, _spaceAgency.CreateRover());
            }

            _spaceAgency.SendCommandsToSpaceStation(station, NavigationCommand);
            station.StartResearching();
        }
    }
}

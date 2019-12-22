using Mars.Rover.Core.Domain;
using Mars.Rover.Core.Domain.Impl;
using Mars.Rover.Core.Extensions;
using Mars.Rover.Tests.Helpers;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Mars.Rover.Tests
{
    [TestFixture]
    public class DomainDesignTests
    {
        const string Commands = @"5 5
							      1 2 N
							      LMLMLMLMM
							      3 3 E
							      MMRMMRMRRM";

        private ServiceProvider _serviceProvider;


        [SetUp]
        public void SetUp()
        {
            _serviceProvider = new ServiceCollection()
               .AddDependencies()
               .BuildServiceProvider();
        }

        [Test]
        public void mars_should_have_a_rectengular_plataeu()
        {
            var mars = _serviceProvider.GetService<IPlanet>();
            mars.Plateau = new PlateauBuilder().Build();

            Assert.IsNotNull(mars.Plateau);
            Assert.IsInstanceOf(typeof(Rectangular), mars.Plateau);
        }

        [Test]
        public void nasa_should_land_rovers_on_mars()
        {
            var nasa = _serviceProvider.GetService<ISpaceAgency>();
            var rover = nasa.CreateRover();
            var mars = _serviceProvider.GetService<IPlanet>();

            Assert.IsNotNull(mars.Rovers);

            int oldRoverCount = mars.Rovers.Count;
            nasa.SendRoverToMars(mars, rover);
            int newRoverCount = mars.Rovers.Count;

            Assert.AreEqual(oldRoverCount + 1, newRoverCount);
        }

        [Test]
        public void rover_camera_should_take_a_photo()
        {
            var rover = new RoverBuilder().Build();

            Assert.IsNotNull(rover.Camera);
            Assert.IsNotNull(rover.Photos);

            int oldPhotoCount = rover.Photos.Count;
            rover.TakePhoto();
            int newPhotoCount = rover.Photos.Count;

            Assert.AreEqual(oldPhotoCount + 1, newPhotoCount);
        }

        [Test]
        public void rover_should_send_photos_to_nasa()
        {
            var nasa = _serviceProvider.GetService<ISpaceAgency>();
            var rover = nasa.CreateRover();

            rover.TakePhoto();

            int oldPhotoCount = rover.Photos.Count;
            rover.SendPhotosToNasa();
            int newPhotoCount = rover.Photos.Count;

            Assert.AreEqual(0, newPhotoCount);
        }

        [Test]
        public void rover_should_have_compasspoint_position_and_x_y_coordinate_location()
        {
            var rover = new RoverBuilder().Build();

            rover.Position = CompassPoint.North;
            rover.Location = new Location { X = 0, Y = 0 };

            Assert.IsNotNull(rover.Position);
            Assert.IsNotNull(rover.Location);
            Assert.AreEqual(rover.Position, CompassPoint.North);
            Assert.AreEqual(rover.Location.X, 0);
            Assert.AreEqual(rover.Location.Y, 0);
        }

        [Test]
        public void plateau_has_a_grid_system()
        {
            var plateau = new PlateauBuilder().Build();
            Assert.IsNotNull(plateau.Grid);
        }

        [Test]
        public void nasa_sends_string_commands_to_control_rovers()
        {
            var nasa = _serviceProvider.GetService<ISpaceAgency>();
            var spaceStation = nasa.CreateSpaceStation();

            int oldResearchInfoCount = spaceStation.ResearchInfos.Count;
            nasa.SendCommandsToSpaceStation(spaceStation, Commands);
            int newResearchInfoCount = spaceStation.ResearchInfos.Count;

            Assert.AreEqual(oldResearchInfoCount + 2, newResearchInfoCount);
        }

        [Test]
        public void space_station_should_define_plataue()
        {
            var nasa = _serviceProvider.GetService<ISpaceAgency>();
            var spaceStation = nasa.CreateSpaceStation();

            nasa.SendCommandsToSpaceStation(spaceStation, Commands);

            Assert.IsNotNull(spaceStation.Mars);
            Assert.AreEqual(spaceStation.Mars.Plateau.Width, 5);
            Assert.AreEqual(spaceStation.Mars.Plateau.Height, 5);
        }

        [Test]
        public void L_command_should_make_rover_spin_lest()
        {
            var nasa = _serviceProvider.GetService<ISpaceAgency>();
            var rover = nasa.CreateRover();
            rover.Position = CompassPoint.North;

            Assert.IsTrue(rover.Spin('L'));
            Assert.AreEqual(rover.Position, CompassPoint.West);
        }

        [Test]
        public void R_command_should_make_rover_spin_lest()
        {
            var nasa = _serviceProvider.GetService<ISpaceAgency>();
            var rover = nasa.CreateRover();
            rover.Position = CompassPoint.North;

            Assert.IsTrue(rover.Spin('R'));
            Assert.AreEqual(rover.Position, CompassPoint.East);
        }

        [Test]
        public void rover_should_move_forward()
        {
            var nasa = _serviceProvider.GetService<ISpaceAgency>();
            var rover = nasa.CreateRover();
            rover.Location.Y = 0;

            Assert.IsTrue(rover.MoveForward());
            Assert.AreEqual(rover.Location.Y, 1);
        }

        [Test]
        public void space_station_should_trigger_the_research_on_mars()
        {
            var nasa = _serviceProvider.GetService<ISpaceAgency>();
            var spaceStation = nasa.CreateSpaceStation();

            for (int i = 0; i < 3; i++)
            {
                nasa.SendRoverToMars(spaceStation.Mars, nasa.CreateRover());
            }

            nasa.SendCommandsToSpaceStation(spaceStation, Commands);

            Assert.IsTrue(spaceStation.StartResearching());
        }
    }
}



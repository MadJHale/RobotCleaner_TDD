using NUnit.Framework;

namespace RobotCleaner.Tests.Robot
{
    [TestFixture]
    public class Tests
    {
        private RobotCleaner.Robot _robot;
        private RobotCleaner.Commander _commander;

        [SetUp]
        public void Setup()
        {
            _commander = new RobotCleaner.Commander();
        }

        //The Robot Will never be sent outside the bounds of the plane. 

        [Test]
        public void RobotWasCreated()
        {
            _commander.AddInput("2");
            _commander.AddInput("10 22");
            _commander.AddInput("E 2");
            _commander.AddInput("N 1");

            _robot = new RobotCleaner.Robot(_commander.GetTheCommands());

            Assert.IsNotNull(_robot);
        }

        [Test]
        public void RobotWasCreated_NoMovementCommands()
        {
            _commander.AddInput("0");
            _commander.AddInput("10 22");

            _robot = new RobotCleaner.Robot(_commander.GetTheCommands());

            _robot.RobotRun();

            Assert.AreEqual(_commander.Commands.StartingPosition.X, _robot.Position.X);
            Assert.AreEqual(_commander.Commands.StartingPosition.Y, _robot.Position.Y);
        }

        [Test]
        public void RobotWasCreated_ResultReturned()
        {
            _commander.AddInput("0");
            _commander.AddInput("10 22");

            _robot = new RobotCleaner.Robot(_commander.GetTheCommands());

            _robot.RobotRun();
            var result = _robot.Report();

            Assert.AreEqual("=> Cleaned: 0", result);
        }

        [Test]
        public void RobotWasCreated_ExampleResultReturned()
        {
            _commander.AddInput("2");
            _commander.AddInput("10 22");
            _commander.AddInput("E 2");
            _commander.AddInput("N 1");

            _robot = new RobotCleaner.Robot(_commander.GetTheCommands());

            _robot.RobotRun();
            var result = _robot.Report();

            Assert.AreEqual("=> Cleaned: 4", result);
        }

        [Test]
        public void RobotWasCreated_OverrunningTrack()
        {
            _commander.AddInput("4");
            _commander.AddInput("10 22");
            _commander.AddInput("N 2");
            _commander.AddInput("S 2");
            _commander.AddInput("E 2");
            _commander.AddInput("W 2");

            _robot = new RobotCleaner.Robot(_commander.GetTheCommands());

            _robot.RobotRun();
            var result = _robot.Report();

            Assert.AreEqual("=> Cleaned: 5", result);
        }

        [Test]
        public void RobotWasCreated_LargeTestTrack()
        {
            _commander.AddInput("12");
            _commander.AddInput("0 0");
            _commander.AddInput("N 9");
            _commander.AddInput("E 1");
            _commander.AddInput("S 9");
            _commander.AddInput("E 1");
            _commander.AddInput("N 9");
            _commander.AddInput("E 1");
            _commander.AddInput("S 9");
            _commander.AddInput("E 1");
            _commander.AddInput("N 9");
            _commander.AddInput("E 1");
            _commander.AddInput("S 9");
            _commander.AddInput("W 5");

            _robot = new RobotCleaner.Robot(_commander.GetTheCommands());

            _robot.RobotRun();
            var result = _robot.Report();

            Assert.AreEqual("=> Cleaned: 60", result);
        }
    }
}
using NUnit.Framework;

namespace RobotCleaner.Tests.Commander
{
    [TestFixture]
    public class Tests
    {
        private RobotCleaner.Commander _commander;
        private Movements _movements;

        [SetUp]
        public void Setup()
        {
            _commander = new RobotCleaner.Commander();
        }

        [Test]
        public void RobotWasCreated_InputsAreCorrect()
        {
            _commander.AddInput("2");
            _commander.AddInput("10 22");
            _commander.AddInput("E 2");
            _commander.AddInput("N 1");

            Assert.IsTrue(_commander.InputsAreGiven);
        }

        [Test]
        public void RobotWasCreated_InputsAreCorrect_NoInputs()
        {
            _commander.AddInput("0");
            _commander.AddInput("10 22");

            Assert.IsTrue(_commander.InputsAreGiven);
        }

        [Test]
        public void RobotWasCreated_InputsAreCorrect_MaxInputs()
        {
            _commander.AddInput("10000");
            _commander.AddInput("10 22");
            for (int i = 0; i < 10000; i++)
            {
                _commander.AddInput("N 1");
            }

            Assert.IsTrue(_commander.InputsAreGiven);
        }

        [Test]
        public void RobotWasCreated_WrongInputValues_NegativeInputs()
        {
            _commander.AddInput("-5");
            _commander.AddInput("10 22");

            Assert.IsTrue(_commander.InputsAreGiven);
        }

        [Test]
        public void RobotWasCreated_WrongInputValues_OverMaxInput()
        {
            _commander.AddInput("10001");
            _commander.AddInput("10 22");
            for (int i = 0; i < 10001; i++)
            {
                _commander.AddInput("N 1");
            }

            Assert.IsTrue(_commander.InputsAreGiven);
        }

        [Test]
        public void RobotWasCreated_InputsAreCorrect_PositionIsSet()
        {
            _commander.AddInput("0");
            _commander.AddInput("10 22");

            Assert.AreEqual(10, _commander.Commands.StartingPosition.X);
            Assert.AreEqual(22, _commander.Commands.StartingPosition.Y);
        }

        [Test]
        public void RobotWasCreated_WrongInputValues_PositionIsOutOfRange()
        {
            _commander.AddInput("0");
            _commander.AddInput("-100001 100001");

            Assert.AreEqual(-100000, _commander.Commands.StartingPosition.X);
            Assert.AreEqual(100000, _commander.Commands.StartingPosition.Y);
        }

        [Test]
        public void RobotWasCreated_InputsAreCorrect_MovementIsGiven()
        {
            _commander.AddInput("2");
            _commander.AddInput("10 22");
            _commander.AddInput("E 2");

            Assert.AreEqual(1, _commander.Commands.MovementList.Count);
        }

        [Test]
        public void RobotWasCreated_WrongInputValues_MovementHasTooManySteps()
        {
            _commander.AddInput("2");
            _commander.AddInput("10 22");
            _commander.AddInput("E 100000");
            _movements = _commander.Commands.MovementList[0];

            Assert.AreEqual(99999, _movements.StepCount);
        }

        [Test]
        public void RobotWasCreated_WrongInputValues_MovementHasNegativeSteps()
        {
            _commander.AddInput("2");
            _commander.AddInput("10 22");
            _commander.AddInput("E -1");
            _movements = _commander.Commands.MovementList[0];

            Assert.AreEqual(1, _movements.StepCount);
        }

        [Test]
        public void RobotCommands_InputsAreCorrect()
        {
            _commander.AddInput("2");
            _commander.AddInput("10 22");
            _commander.AddInput("E 2");
            _commander.AddInput("N 1");

            Assert.IsNotNull(_commander.GetTheCommands());
        }

        [Test]
        public void RobotCommands_InputsAreCorrect_MovementHas2Commands()
        {
            _commander.AddInput("2");
            _commander.AddInput("10 22");
            _commander.AddInput("E 2");
            _commander.AddInput("N 1");

            Assert.AreEqual(2, _commander.GetTheCommands().MovementList.Count);
        }

        [Test]
        public void RobotCommands_WrongInputValues_Missing2MovementCommands()
        {
            _commander.AddInput("2");
            _commander.AddInput("10 22");

            Assert.IsNull(_commander.GetTheCommands());
        }
    }
}
using System.Collections.Generic;

namespace RobotCleaner
{
    public class Commander
    {
        private readonly List<string> _inputs;

        public Commander()
        {
            _inputs = new List<string>();
            Commands = new Commands();
        }

        public bool InputsAreGiven => _inputs.Count == (Commands.NumOfInputs + 2);

        public Commands Commands { get; }

        public Commands GetTheCommands()
        {
            return InputsAreGiven ? Commands : null;
        }

        public void AddInput(string input)
        {
            if (InputsAreGiven) return;
            if (_inputs.Count == 0)
            {
                SetNumOfCommands(input);
            } else if(_inputs.Count == 1)
            {
                SetPositionOfRobot(input);
            } else
            {
                Commands.MovementList.Add(SetMovement(input));
            }
            _inputs.Add(input);
        }

        private static Movements SetMovement(string input)
        {
            var robotMovement = new Movements();

            var movementString = input.Split(null);
            if (movementString.Length <= 1) return robotMovement;
            switch (movementString[0]) {
                case "N":
                    robotMovement.Direction = Direction.North;
                    break;
                case "S":
                    robotMovement.Direction = Direction.South;
                    break;
                case "E":
                    robotMovement.Direction = Direction.East;
                    break;
                case "W":
                    robotMovement.Direction = Direction.West;
                    break;                    
            }
            var stepCount = int.Parse(movementString[1]);
            if(stepCount > 99999)
            {
                robotMovement.StepCount = 99999;
            } else if (stepCount < 1)
            {
                robotMovement.StepCount = 1;
            } else
            {
                robotMovement.StepCount = stepCount;
            }
            return robotMovement;
        }

        private void SetPositionOfRobot(string input)
        {
            var positionString = input.Split(null);
            if (positionString.Length <= 1) return;
            var x = int.Parse(positionString[0]);
            var y = int.Parse(positionString[1]);
            Commands.StartingPosition = new Position(KeepNumInRange(x), KeepNumInRange(y));
        }

        private static int KeepNumInRange(int num)
        {
            if (num < -100000)
            {
                num = -100000;
            }
            if (num > 100000)
            {
                num = 100000;
            }

            return num;
        }

        private void SetNumOfCommands(string input)
        {
            Commands.NumOfInputs = int.Parse(input);
            if(Commands.NumOfInputs < 0)
            {
                Commands.NumOfInputs = 0;
            }
            if(Commands.NumOfInputs > 10000)
            {
                Commands.NumOfInputs = 10000;
            }
        }
    }
}
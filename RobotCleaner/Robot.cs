using System;
using System.Collections.Generic;

namespace RobotCleaner
{
    public class Robot
    {
        public Position Position { get; set; }
        public List<Movements> Movements { get; set; }
        public List<Position> UniqueLocationsVisited { get; set; }

        public Robot(Commands commands)
        {
            Position = commands.StartingPosition;
            Movements = commands.MovementList;
            UniqueLocationsVisited = new List<Position>{Position};
        }
        
        public void RobotRun()
        {
            foreach (var movement in Movements)
            {
                for (var i = 0; i < movement.StepCount; i++)
                {
                    MoveRobot(movement);
                }                
            }
        }

        private void MoveRobot(Movements movement)
        {
            switch (movement.Direction)
            {
                case Direction.North:
                    Position = new Position(Position.X, Position.Y + 1);
                    break;
                case Direction.South:
                    Position = new Position(Position.X, Position.Y - 1);
                    break;
                case Direction.East:
                    Position = new Position(Position.X + 1, Position.Y);
                    break;
                case Direction.West:
                    Position = new Position(Position.X - 1, Position.Y);
                    break;
            }

            if (!UniqueLocationsVisited.Contains(Position))
            {
                UniqueLocationsVisited.Add(Position);
            }
        }

        public string Report()
        {
            return Movements.Count > 0 ? $"=> Cleaned: {UniqueLocationsVisited.Count}" : "=> Cleaned: 0";
        }
    }
}

using System.Collections.Generic;

namespace RobotCleaner
{
    public class Commands
    {
        public int NumOfInputs { get; set; }

        public Position StartingPosition { get; set; }

        public List<Movements> MovementList { get; }

        public Commands()
        {
            MovementList = new List<Movements>();
        }
    }
}
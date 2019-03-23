namespace RobotCleaner.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            var robotCommander = new Commander();

            while (!robotCommander.InputsAreGiven)
            {
                //if (robotCommander.Commands.NumOfInputs == 0)
                //{
                //    System.Console.WriteLine("Please enter the number of movement commands");
                //} else if (robotCommander.Commands.StartingPosition == null)
                //{
                //    System.Console.WriteLine("Please enter the starting position in the format 'x y'");
                //} else if (robotCommander.Commands.MovementList.Count != robotCommander.Commands.NumOfInputs)
                //{
                //    System.Console.WriteLine("Please enter one movement command in the format 'DIRECTION STEPS' like this 'N 2'");
                //}
                robotCommander.AddInput(System.Console.ReadLine()?.Trim());
            }

            var robot = new Robot(robotCommander.GetTheCommands());

            robot.RobotRun();
            System.Console.WriteLine(robot.Report());
            System.Console.ReadKey();
        }
    }
}

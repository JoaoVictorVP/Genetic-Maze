using GeneticMaze;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

Console.WriteLine("Genetic Algorithm");

void DrawMaze(Maze maze, (int x, int y) endPoint, Walker fitest)
{
    //StringBuilder map = new StringBuilder(maze.Width * maze.Height);
    int w = maze.Width, h = maze.Height;
    for (int y = 0; y < h; y++)
    {
        Console.Write('\n');
        for (int x = 0; x < w; x++)
        {
            var tile = maze[x, y];

            ConsoleColor ck;

            if (x == fitest.X && y == fitest.Y)
                ck = ConsoleColor.Green;
            else if (x == endPoint.x && y == endPoint.y)
                ck = ConsoleColor.Red;
            else
                ck = tile switch
                {
                    Tile.Normal => ConsoleColor.White,
                    Tile.Wall => ConsoleColor.DarkGray
                };

            Console.BackgroundColor = ck;
            Console.Write(' ');
        }
    }
    Console.ResetColor();
}

var map = MazeGenerator.FromImage("Maze.png");

var nature = new Nature
{
    Maze = map.maze,
    StartPoint = map.startPoint,
    EndPoint = map.endPoint
};

const int Generations = 30,
    TotalWalkers = 30;

var sw = new Stopwatch();
sw.Start();
var results = nature.Generate(Generations, TotalWalkers);
sw.Stop();

Console.WriteLine(@$"--- ENDING ---
Elapsed Time: {sw.Elapsed}
Total Iterations: {results.iterations}
Best Walker: {JsonSerializer.Serialize(results.best)}

Maze:::");
DrawMaze(map.maze, map.endPoint, results.best);
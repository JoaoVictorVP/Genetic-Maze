namespace GeneticMaze;

public class Nature
{
    public readonly Random Random = new Random();

    public Maze Maze;

    public (int x, int y) StartPoint, EndPoint;

    public (int iterations, Walker best) Generate(int generations, int totalWalkers)
    {
        Walker fitest = default;
        int totalIterations = 0;
        for(int i = 0; i < generations; i++)
        {
            var gen = new Generation(this, totalWalkers, fitest);
            (int iterations, fitest) = gen.Run();
            totalIterations += iterations;
        }
        return (totalIterations, fitest);
    }
}
public class Generation
{
    public readonly Nature Nature;
    public readonly int Count;
    public readonly Walker[] Walkers;

    public (int iterations, Walker fitest) Run()
    {
        var map = Nature.Maze;

        int iterations = 0;
    iterate:
        iterations++;
        int alive = 0;
        for(int i = 0; i < Count; i++)
        {
            ref var walker = ref Walkers[i];
            if (walker.IsAlive) alive++;

            var dir = walker.PickDirection(Nature);
            (int x, int y) moveTo = (walker.X + dir.x, walker.Y + dir.y);

            if (map.Inside(moveTo) && map[moveTo] == Tile.Normal)
            {
                walker.X = moveTo.x;
                walker.Y = moveTo.y;
            }

            walker.CurSteps--;
        }
        if (alive > 0)
            goto iterate;

        var endPoint = Nature.EndPoint;
        ref var fitest = ref Walkers[0];
        double curMin = int.MaxValue;
        foreach(var walker in Walkers)
        {
            double dist = map.Distance((walker.X, walker.Y), endPoint);
            if(dist < curMin)
            {
                fitest = walker;
                curMin = dist;
            }
        }

        return (iterations, fitest);
    }

    void initialize(ref Walker bestFitBefore)
    {
        var map = Nature.Maze;

        var rand = Nature.Random;

        var begin = Nature.StartPoint;
        if (!map.Inside(begin)) begin = (0, 0);

        for(int i = 0; i < Count; i++)
        {
            var walker = new Walker();
            walker.InitializeForGeneration(Nature, rand, map, bestFitBefore);
            Walkers[i] = walker;
        }
    }

    public Generation(Nature nature, int count, Walker bestFitBefore)
    {
        Nature = nature;
        Count = count;
        Walkers = new Walker[count];

        initialize(ref bestFitBefore);
    }
}
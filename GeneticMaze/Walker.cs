namespace GeneticMaze;

public struct Walker
{
    #region Instance
    public int X, Y;
    public uint CurSteps;
    public bool IsAlive => CurSteps > 0;
    #endregion

    #region Evolution
    public uint Steps;
    public double Left, Right, Top, Bottom;
    #endregion

    public bool IsEmpty => Steps == 0 && Left == 0 && Right == 0 && Top == 0 && Bottom == 0;

    public void InitializeForGeneration(Nature nature, Random random, Maze maze, Walker model)
    {
        if(model.IsEmpty)
        {
            Steps = (uint)random.Next(0, nature.Maze.Width * nature.Maze.Height);
            Left = random.NextDouble();
            Right = random.NextDouble();
            Top = random.NextDouble();
            Bottom = random.NextDouble();
        }
        else
        {
            Steps = model.Steps;
            Left = model.Left;
            Right = model.Right;
            Top = model.Top;
            Bottom = model.Bottom;

            if (random.NextDouble() > 0.5)
            {
                if (random.NextDouble() > 0.5) Steps += (uint)random.Next(0, nature.Maze.Width * nature.Maze.Height);
                else Steps -= (uint)random.Next(0, nature.Maze.Width * nature.Maze.Height);
            }
            if (random.NextDouble() > 0.5)
            {
                if (random.NextDouble() > 0.5) Left += random.NextDouble();
                else Left -= random.NextDouble();
                if (Left < 0) Left = random.NextDouble();
            }
            if (random.NextDouble() > 0.5)
            {
                if (random.NextDouble() > 0.5) Right += random.NextDouble();
                else Right -= random.NextDouble();
                if (Right < 0) Right = random.NextDouble();
            }
            if (random.NextDouble() > 0.5)
            {
                if (random.NextDouble() > 0.5) Top += random.NextDouble();
                else Top -= random.NextDouble();
                if (Top < 0) Top = random.NextDouble();
            }
            if (random.NextDouble() > 0.5)
            {
                if (random.NextDouble() > 0.5) Bottom += random.NextDouble();
                else Bottom -= random.NextDouble();
                if (Bottom < 0) Bottom = random.NextDouble();
            }
        }

        CurSteps = Steps;
    }

    public (int x, int y) PickDirection(Nature nature)
    {
        var rand = nature.Random;

        var chance = rand.NextDouble();

        if (chance < Left)
            return (-1, 0);
        else if (chance < Right)
            return (1, 0);
        else if (chance < Top)
            return (0, 1);
        else if (chance < Bottom)
            return (0, -1);

        return default;
    }
}

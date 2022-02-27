namespace GeneticMaze;

public class Maze
{
    public readonly int Width, Height;

    public readonly byte[,] Map;

    public Tile this[(int x, int y) point]
    {
        get => this[point.x, point.y];
        set => this[point.x, point.y] = value;
    }
    public Tile this[int x, int y]
    {
        get => (Tile) (inBounds(x, y) ? Map[x, y] : -1);
        set
        {
            if (value != Tile.OutOfBounds && inBounds(x, y)) Map[x, y] = (byte)value;
        }
    }
    bool inBounds(int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
            return false;
        return true;
    }

    public double Distance((int x, int y) from, (int x, int y) to) => Math.Sqrt((Math.Pow(from.x - to.x, 2) + Math.Pow(from.y - to.y, 2)));

    public bool Inside((int x, int y) point) => Inside(point.x, point.y);
    public bool Inside(int x, int y) => inBounds(x, y);

    public Maze(int width, int height)
    {
        Width = width;
        Height = height;
        Map = new byte[Width, Height];
    }
}
public enum Tile : int
{
    OutOfBounds = -1,
    Normal,
    Wall
}
using GeneticMaze;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

public static class MazeGenerator
{
    public static (Maze maze, (int x, int y) startPoint, (int x, int y) endPoint) FromImage(string path)
    {
        using Image<Rgba32> image = (Image<Rgba32>)Image.Load(path);
        int w = image.Width, h = image.Height;

        (int x, int y) sp = default, ep = default;

        Maze map = new Maze(w, h);
        for (int x = 0; x < w; x++)
            for (int y = 0; y < h; y++)
            {
                var pixel = image[x, y];

                if (pixel.R == 0 && pixel.G == 0 && pixel.B == 0)
                    map[x, y] = Tile.Wall;
                else if (pixel.R == 1 && pixel.G == 1 && pixel.B == 1)
                    map[x, y] = Tile.Normal;
                else if (pixel.R == 0 && pixel.G == 1 && pixel.B == 0)
                {
                    map[x, y] = Tile.Normal;
                    sp = (x, y);
                }
                else if (pixel.R == 1 && pixel.G == 0 && pixel.B == 0)
                {
                    map[x, y] = Tile.Normal;
                    ep = (x, y);
                }
            }

        return (map, sp, ep);
    }
}

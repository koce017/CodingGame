using UnityEngine;

public class Maze
{
    internal int W => Tiles.GetLength(1);
    internal int H => Tiles.GetLength(0);
    internal string[,] Tiles { get; private set; }
    internal Vector2Int Exit { get; private set; } = new Vector2Int();

    internal Vector2Int Spawn { get; private set; } = new Vector2Int();

    internal Maze(int w, int h)
    {
        Tiles = new string[h, w];
    }

    internal void SetExit(int r, int c)
    {
        Exit = new Vector2Int(c, r);
    }

    internal void SetSpawn(int r, int c)
    {
        Spawn = new Vector2Int(c, r);
    }

    public object GetTile(object[] args)
    {
        return Tiles[(int)args[1], (int)args[0]];
    }
}

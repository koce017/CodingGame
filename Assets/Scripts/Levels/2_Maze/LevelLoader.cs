using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public Texture2D map;
    public TileSize tileSize;
    public float tileSpacing;
    public MazeObject[] levelObjects;

    internal Maze Maze { get; private set; }

    void Awake()
    {
        Maze = new Maze(map.width, map.height);

        Dictionary<Color, MazeObject> mappings = new();
        foreach (var levelObject in levelObjects)
            mappings.Add(levelObject.mapColor, levelObject);

        for (var r = 0; r < map.height; ++r)
        {
            for (var c = 0; c < map.width; ++c)
            {
                var levelObject = mappings[map.GetPixel(c, r)];
                InstantiateTile(r, c, levelObject);
                if (levelObject.prefab.name == "Spawner")
                    Maze.SetSpawn(r, c);
                else if (levelObject.prefab.name == "Exit")
                    Maze.SetExit(r, c);
            }
        }
    }

    private GameObject InstantiateTile(int r, int c, MazeObject levelObject)
    {
        var position = GetTilePosition(r, c, levelObject.heightScale * 0.5f);
        var obj = Instantiate(levelObject.prefab, position, Quaternion.identity, transform);
        obj.transform.localScale = new Vector3(tileSize.length, tileSize.height * levelObject.heightScale, tileSize.length);
        Maze.Tiles[r, c] = levelObject.prefab.name;
        return obj;
    }

    internal Vector3 GetTilePosition(int r, int c, float y)
    {
        return new Vector3(c * (tileSize.length + tileSpacing), y, r * (tileSize.width + tileSpacing));
    }
}
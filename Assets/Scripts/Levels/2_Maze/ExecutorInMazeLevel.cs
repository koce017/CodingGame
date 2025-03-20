using UnityEngine;
using Kostic017.Pigeon;
using Kostic017.Pigeon.Symbols;
using System.Collections.Generic;

public class ExecutorInMazeLevel : Executor
{
    public GameObject robotPrefab;
    public LevelLoader levelLoader;

    private readonly Dictionary<string, object> globals = new();

    internal override void Run()
    {
        base.Run();
        var go = Instantiate(robotPrefab, levelLoader.GetTilePosition(levelLoader.Maze.Spawn.y, levelLoader.Maze.Spawn.x, 1f), Quaternion.identity);
        var robot = go.GetComponent<RobotInMazeLevel>();
        robot.SetPosition(levelLoader.Maze.Spawn.y, levelLoader.Maze.Spawn.x);
        StartExecution(robot);
    }

    internal override void Stop()
    {
        base.Stop();
        globals.Clear();
        Destroy(FindObjectOfType<RobotInMazeLevel>().gameObject);
    }

    internal void StartExecution(RobotInMazeLevel robot)
    {
        var b = new Builtins();

        b.RegisterVariable(PigeonType.Int, "EXIT_COL", true, levelLoader.Maze.Exit.x);
        b.RegisterVariable(PigeonType.Int, "EXIT_ROW", true, levelLoader.Maze.Exit.y);

        b.RegisterVariable(PigeonType.Int, "LEVEL_WIDTH", true, levelLoader.Maze.W);
        b.RegisterVariable(PigeonType.Int, "LEVEL_HEIGHT", true, levelLoader.Maze.H);

        b.RegisterFunction(PigeonType.Int, "row", robot.R);
        b.RegisterFunction(PigeonType.Int, "col", robot.C);

        b.RegisterFunction(PigeonType.Void, "move_up", robot.MoveUp);
        b.RegisterFunction(PigeonType.Void, "move_down", robot.MoveDown);
        b.RegisterFunction(PigeonType.Void, "move_left", robot.MoveLeft);
        b.RegisterFunction(PigeonType.Void, "move_right", robot.MoveRight);

        b.RegisterFunction(PigeonType.String, "get_tile", levelLoader.Maze.GetTile, PigeonType.Int, PigeonType.Int);

        b.RegisterFunction(PigeonType.Int, "string_len", PigeonString.Length, PigeonType.String);
        b.RegisterFunction(PigeonType.String, "string_char", PigeonString.Char, PigeonType.String, PigeonType.Int);

        b.RegisterFunction(PigeonType.Int, "set_create", PigeonSet.Create);
        b.RegisterFunction(PigeonType.Void, "set_destroy", PigeonSet.Destroy, PigeonType.Int);
        b.RegisterFunction(PigeonType.Void, "set_add", PigeonSet.Add, PigeonType.Int, PigeonType.Any);
        b.RegisterFunction(PigeonType.Any, "set_remove", PigeonSet.Remove, PigeonType.Int, PigeonType.Any);
        b.RegisterFunction(PigeonType.Bool, "set_in", PigeonSet.In, PigeonType.Int, PigeonType.Any);

        b.RegisterFunction(PigeonType.Void, "global_set", GlobalSet, PigeonType.String, PigeonType.Any);
        b.RegisterFunction(PigeonType.Void, "global_unset", GlobalUnset, PigeonType.String);
        b.RegisterFunction(PigeonType.Bool, "global_check", GlobalCheck, PigeonType.String);
        b.RegisterFunction(PigeonType.Any, "global_get", GlobalGet, PigeonType.String);

        b.RegisterFunction(PigeonType.Void, "print", Print, PigeonType.Any);

        Execute(new Interpreter(codeEditor.Code, b));
    }
    
    public object GlobalSet(object[] args)
    {
        globals[(string)args[0]] = args[1];
        return null;
    }

    public object GlobalCheck(object[] args)
    {
        return globals.ContainsKey((string)args[0]);
    }

    public object GlobalUnset(object[] args)
    {
        globals.Remove((string)args[0]);
        return null;
    }

    public object GlobalGet(object[] args)
    {
        return globals[(string)args[0]];
    }
}

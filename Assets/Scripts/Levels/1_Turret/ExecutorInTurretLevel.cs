using Kostic017.Pigeon;
using Kostic017.Pigeon.Symbols;
using UnityEngine;

public class ExecutorInTurretLevel : Executor
{
    private Turret turret;
    private RobotInTurretLevel[] robots;

    private void Awake()
    {
        turret = FindObjectOfType<Turret>();
        robots = FindObjectsOfType<RobotInTurretLevel>();
    }

    internal override void Run()
    {
        base.Run();
        StartExecution(turret);
    }

    internal override void Stop()
    {
        base.Stop();
        turret.Reset();
        foreach (var robot in robots)
            robot.Reset();
    }

    internal void StartExecution(Turret turret)
    {
        var b = new Builtins();

        b.RegisterFunction(PigeonType.Float, "x", turret.X);
        b.RegisterFunction(PigeonType.Float, "y", turret.Y);

        b.RegisterFunction(PigeonType.Int, "robot_count", turret.RobotCount);
        b.RegisterFunction(PigeonType.Float, "robot.x", turret.RobotX, PigeonType.Int);
        b.RegisterFunction(PigeonType.Float, "robot.y", turret.RobotX, PigeonType.Int);

        b.RegisterFunction(PigeonType.Void, "shoot", turret.Shoot, PigeonType.Int);

        b.RegisterFunction(PigeonType.Int, "list_create", PigeonList.Create);
        b.RegisterFunction(PigeonType.Void, "list_destroy", PigeonList.Destroy, PigeonType.Int);
        b.RegisterFunction(PigeonType.Void, "list_add", PigeonList.Add, PigeonType.Int, PigeonType.Any);
        b.RegisterFunction(PigeonType.Any, "list_get", PigeonList.Get, PigeonType.Int, PigeonType.Int);
        b.RegisterFunction(PigeonType.Void, "list_set", PigeonList.Set, PigeonType.Int, PigeonType.Int, PigeonType.Any);
        b.RegisterFunction(PigeonType.Int, "list_count", PigeonList.Count, PigeonType.Int);

        b.RegisterFunction(PigeonType.Float, "sqrt", Sqrt, PigeonType.Float);

        b.RegisterFunction(PigeonType.Void, "print", Print, PigeonType.Any);

        Execute(new Interpreter(codeEditor.Code, b));
    }

    public object Sqrt(object[] args)
    {
        return Mathf.Sqrt((float)args[0]);
    }

}
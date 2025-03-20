using Kostic017.Pigeon;
using Kostic017.Pigeon.Symbols;
using UnityEngine;

public class ExecutorInTurretLevel : Executor
{
    private Turret turret;
    private RobotInTurretLevel[] robots;

    private void Awake()
    {
        turret = FindFirstObjectByType<Turret>();
        robots = FindObjectsByType<RobotInTurretLevel>(FindObjectsSortMode.None);
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
        var b = new BuiltinBag();

        b.RegisterFunction(PigeonType.Float, "x", turret.X);
        b.RegisterFunction(PigeonType.Float, "y", turret.Y);

        b.RegisterFunction(PigeonType.Int, "robot_count", turret.RobotCount);
        b.RegisterFunction(PigeonType.Float, "robot_x", turret.RobotX, PigeonType.Int);
        b.RegisterFunction(PigeonType.Float, "robot_y", turret.RobotX, PigeonType.Int);
        
        b.RegisterFunction(PigeonType.Void, "shoot", turret.Shoot, PigeonType.Int);
        
        b.RegisterFunction(PigeonType.Float, "sqrt", Sqrt, PigeonType.Float);

        b.RegisterFunction(PigeonType.Void, "print", Print, PigeonType.Int);
        b.RegisterFunction(PigeonType.Void, "print", Print, PigeonType.Float);
        b.RegisterFunction(PigeonType.Void, "print", Print, PigeonType.String);
        b.RegisterFunction(PigeonType.Void, "print", Print, PigeonType.Bool);

        Execute(new Interpreter(codeEditor.Code, b));
    }

    public object Sqrt(object[] args)
    {
        return Mathf.Sqrt((float)args[0]);
    }

}
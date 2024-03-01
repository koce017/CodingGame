using System.Collections.Generic;

public static class PigeonList
{

    private static readonly List<List<object>> lists = new();

    public static object Create(object[] _)
    {
        lists.Add(new List<object>());
        return lists.Count - 1;
    }

    public static object Destroy(object[] args)
    {
        lists[(int)args[0]] = null;
        return null;
    }

    public static object Add(object[] args)
    {
        List(args).Add(args[1]);
        return null;
    }

    public static object Get(object[] args)
    {
        return List(args)[(int)args[1]];
    }

    public static object Set(object[] args)
    {
        List(args)[(int)args[1]] = args[2];
        return null;
    }

    public static object Count(object[] args)
    {
        return List(args).Count;
    }

    private static List<object> List(object[] args)
    {
        return lists[(int)args[0]];
    }

    public static void Clear()
    {
        lists.Clear();
    }

}

using Kostic017.Pigeon;
using System;
using System.IO;
using UnityEngine;

public class Executor : MonoBehaviour
{
    public EditButton editButton;
    public PigeonCodeEditor codeEditor;

    internal bool IsRunning { get; private set; }

    internal virtual void Run()
    {
        IsRunning = true;
        editButton.gameObject.SetActive(false);
    }

    internal virtual void Stop()
    {
        IsRunning = false;
        editButton.gameObject.SetActive(true);
    }

    protected virtual void Execute(Interpreter interpreter)
    {
        if (interpreter.HasNoErrors())
            try
            {
                interpreter.Evaluate();
            }
            catch (Exception e)
            {
                Stop();
                Debug.LogError(e.Message);
            }
        else
        {
            Stop();
            PrintErrors(interpreter);
        }
    }

    protected void PrintErrors(Interpreter interpreter)
    {
        var sw = new StringWriter();
        interpreter.PrintErr(sw);
        Debug.LogError(sw.ToString());
    }

    public object Print(object[] args)
    {
        print(args[0].ToString());
        return null;
    }
}
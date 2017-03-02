using Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Task
{
    public TaskName Name;
    public Func<bool> Method;

    public Task(TaskName name , Func<bool> method)
    {
        Name = name;
        Method = method;
    }
}

using Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TaskList
{
    private List<Task> _taskList = new List<Task>();

    public Task Add(TaskName name, Func<bool> method)
    {
        var task = _taskList.FirstOrDefault(t => t.Name.Equals(name));
        if (task != null) return task;

        task = new Task(name, method);
        _taskList.Add(task);
        return task;
    }

    public void Remove(TaskName name)
    {
        var task = _taskList.FirstOrDefault(t => t.Name.Equals(name));
        if (task == null) return;

        _taskList.Remove(task);
    }

    public void ExecuteAll()
    {
        foreach(var task in _taskList)
        {
            task.Method();
        }
    }
}
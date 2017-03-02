using Constants;
using System;
using Assets.Toolbox;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GameObjectiveFactory
{
    private Toolbox _toolbox;
    
    public GameObjectiveFactory(Toolbox toolbox)
    {
        _toolbox = toolbox;
    }

    public GameObjective Create(OBJECTIVE objectiveEnum, Func<bool> isComplete)
    {
        switch (objectiveEnum)
        {
            case OBJECTIVE.LOCATE:
                return new GameLocateObjective(_toolbox, isComplete);
            case OBJECTIVE.DESCRIBE:
                return new GameDescribeObjective(_toolbox, isComplete);
            default:
                return new GameObjective(_toolbox, isComplete);
        }
    }
}

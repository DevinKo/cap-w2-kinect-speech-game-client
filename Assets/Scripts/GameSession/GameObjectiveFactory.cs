﻿using Constants;
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

    public GameObjective Create(OBJECTIVE objectiveEnum)
    {
        switch (objectiveEnum)
        {
            case OBJECTIVE.LOCATE:
                return new GameLocateObjective(_toolbox);
            case OBJECTIVE.DESCRIBE:
                return new GameDescribeObjective(_toolbox);
            default:
                return new GameObjective(_toolbox);
        }
    }
}

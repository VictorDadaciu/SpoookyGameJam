using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Actions
{
    MoveCamFwd,
    MoveCamBack,
    MoveCamRight,
    MoveCamLeft
}

public class Configs
{
    public static Dictionary<Actions, KeyCode> actions = new Dictionary<Actions, KeyCode>
    {
        { Actions.MoveCamFwd, KeyCode.W },
        { Actions.MoveCamBack, KeyCode.S },
        { Actions.MoveCamRight, KeyCode.D },
        { Actions.MoveCamLeft, KeyCode.A },
    };
}


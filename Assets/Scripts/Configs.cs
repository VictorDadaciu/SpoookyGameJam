using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Keys
{
    MoveCamFwd,
    MoveCamBack,
    MoveCamRight,
    MoveCamLeft
}

public class Configs
{
    public static Dictionary<Keys, KeyCode> actions = new Dictionary<Keys, KeyCode>
    {
        { Keys.MoveCamFwd, KeyCode.W },
        { Keys.MoveCamBack, KeyCode.S },
        { Keys.MoveCamRight, KeyCode.D },
        { Keys.MoveCamLeft, KeyCode.A },
    };
}


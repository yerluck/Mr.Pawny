using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : Singleton<GameEvents>
{
    protected GameEvents() {}

    public event Action onBridgeTriggerEnter;
    public void BridgeTriggerEnter()
    {
        if (onBridgeTriggerEnter != null)
        {
            onBridgeTriggerEnter();
        }
    }
}

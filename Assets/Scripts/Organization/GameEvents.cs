using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : Singleton<GameEvents>
{
    protected GameEvents() {}

    public event Action onBridgeTriggerEnter;
    public event Action onPlayerFlip;
    public event Action onPlayerJump;

    public void PlayerJump()
    {
        if (onPlayerJump != null)
        {
            onPlayerJump();
        }
    }

    public void BridgeTriggerEnter()
    {
        if (onBridgeTriggerEnter != null)
        {
            onBridgeTriggerEnter();
        }
    }

    public void PlayerFlip()
    {
        if (onPlayerFlip != null)
        {
            onPlayerFlip();
        }
    }
}

using System;

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

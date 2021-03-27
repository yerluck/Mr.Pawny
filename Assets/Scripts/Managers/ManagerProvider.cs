using System;

public static class ManagerProvider
{
    public static T GetManager<T>() where T: Singleton<T>
    {
        T manager = null;

        if(typeof(T) == typeof(PawnEnemyManager))
        {
            manager = PawnEnemyManager.Instance as T;
        }
        if(typeof(T) == typeof(PlayerManager))
        {
            manager = PlayerManager.Instance as T;
        }

        return manager;
    }

    public static IEnemyCharacterManager GetManagerByType(Type type)
    {
        IEnemyCharacterManager manager = null;

        if(type == typeof(PawnEnemyManager))
        {
            manager = PawnEnemyManager.Instance as IEnemyCharacterManager;
        }
        if(type == typeof(PlayerManager))
        {
            manager = PlayerManager.Instance as IEnemyCharacterManager;
        }

        return manager;
    }
}
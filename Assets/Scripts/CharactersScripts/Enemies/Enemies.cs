using System.Collections.Generic;
using System;

public enum EnemyAnimationOverrideKeys
{
    Idle,
    Rage,
    RageRun,
    Run,
    Sleep,
    Warned,
}

public enum WeaponType
{
    Melee,
    Ranged,
}

public class Enemies
{
    public enum EnemyName
    {
        PawnEnemy,
    }


    public static Dictionary<EnemyName, IEnemyCharacterManager> EnemyNameToManager = new Dictionary<EnemyName, IEnemyCharacterManager>
    {
        {EnemyName.PawnEnemy, PawnEnemyManager.Instance}
    };

    public static Dictionary<EnemyName, Type> EnemyNameToType = new Dictionary<EnemyName, Type>
    {
        {EnemyName.PawnEnemy, typeof(PawnEnemy)}
    };
}
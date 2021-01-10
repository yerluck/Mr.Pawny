using System.Collections.Generic;

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
}
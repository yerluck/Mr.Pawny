using UnityEngine;

public class Sense : MonoBehaviour
{
    protected virtual float DetectionRate { get; set; }

    [SerializeField] protected Enemies.EnemyName enemyName;
    protected Aspect.AspectTypes aspectName;
    protected IEnemyCharacterManager manager;
    protected float elapsedTime = 0f;

    protected virtual void Initialize()
    {
        aspectName = GetComponent<Aspect>().aspectType;
        manager = Enemies.EnemyNameToManager[enemyName];
    }
    
    protected virtual void UpdateSense()
    {
        elapsedTime += Time.deltaTime;
    }

    protected void Start()
    {
        elapsedTime = 0f;
        Initialize();    
    }

    void Update()
    {
        UpdateSense();    
    }
}

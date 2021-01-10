using UnityEngine;

public abstract class Sense : MonoBehaviour
{
    protected abstract float DetectionRate { get; set; }

    [SerializeField] protected Enemies.EnemyName enemyName;
    public readonly Aspect.AspectTypes aspectName = Aspect.AspectTypes.ENEMY;
    public bool enableDebug = true;
    protected float elapsedTime = 0f;

    protected abstract void Initialize();
    protected abstract void UpdateSense();

    protected virtual void Start()
    {
        elapsedTime = 0f;
        Initialize();    
    }

    void Update()
    {
        UpdateSense();    
    }
}

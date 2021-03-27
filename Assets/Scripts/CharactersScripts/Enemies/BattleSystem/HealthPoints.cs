using UnityEngine;

public class HealthPoints : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected ScriptableObject statsSO;
    [SerializeField]
    protected HealthBar healthBar;
    [SerializeField]
    protected bool showHealthBar = false;
    [SerializeField]
    protected GameObject HealthBarObject;
    protected IEnemyCommonStats _stats;
    protected float _hitPoints;

    public float HP
    {
        get => _hitPoints;
        set => _hitPoints = value;
    }
    public Transform Attacker
    {
        get;
        set;
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void TakeDamage(float damage)
    {
        HP -= damage;
        DamageEffect();
        if (HP <= 0)
        {
            Die();
        }
    }

    protected virtual void DamageEffect()
    {
        healthBar?.SetHealth(HP);
    }

    protected virtual void Awake()
    {
        _stats = (IEnemyCommonStats)statsSO;
        _hitPoints = _stats.MaxHitPoints;
        healthBar?.SetMaxHealth(_stats.MaxHitPoints);
        HealthBarObject?.SetActive(showHealthBar);
    }
}

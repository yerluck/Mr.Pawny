using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class HealthPoints : MonoBehaviour, IDamageable
{
    [SerializeField] ScriptableObject statsSO;
    [SerializeField] HealthBar healthBar;
    [SerializeField] bool showHealthBar = false;
    [SerializeField] GameObject healthBarObject;
    IEnemyCommonStats _stats;
    float _hitPoints;
    Dictionary<SpriteRenderer, Color> _cachedColors = new Dictionary<SpriteRenderer, Color>();

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

    //---------------TODO: temporary, change to event system-----------
    [SerializeField] GameObject deadBody;
    public virtual void Die()
    {
        var corpse = Instantiate(deadBody, transform.position, transform.rotation, transform.parent);
        var deathHandler = corpse.GetComponent<DeathHandlerBase>();
        deathHandler?.StartDeathAction(new DeathHandlerBase.DeathProperties(transform, Attacker));
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Die();
        }
        else
        {
            DamageEffect();
        }
    }

    protected virtual void DamageEffect()
    {
        healthBar?.SetHealth(HP);
        StopAllCoroutines();
        StartCoroutine(Flash(false, Color.red));
    }

    private void Awake()
    {
        _stats = (IEnemyCommonStats)statsSO;
        _hitPoints = _stats.MaxHitPoints;
        healthBar?.SetMaxHealth(_stats.MaxHitPoints);
        healthBarObject?.SetActive(showHealthBar);
    }

    private void Start()
    {
        foreach(var renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            if(!_cachedColors.ContainsKey(renderer))
            {
                _cachedColors.Add(renderer, renderer.color);
            }
        }
    }

    protected IEnumerator Flash(bool isRepeatedly, Color flashColor)
    {
        int flashCount = isRepeatedly? 4 : 1;
        float flashInterval = 0.3f;
        while(flashCount > 0)
        {
            //Flash
            foreach(var renderer in _cachedColors.Keys)
            {
                renderer.color = flashColor;
            }
            yield return new WaitForSeconds(0.1f);

            // Return color back
            foreach(var renderer in _cachedColors.Keys)
            {
                renderer.color = _cachedColors[renderer];
            }

            if(isRepeatedly)
            {
                yield return new WaitForSeconds(flashInterval);
                flashInterval -= 0.075f;
            }
            flashCount--;
        }
        //StopCoroutine("Flash");
    }
}

using UnityEngine;
using Pawny.AdditionalAction;
using Moment = Pawny.AdditionalAction.AdditionalActionSO.SpecificMoment;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
public class FightSystem : MonoBehaviour, IAttacker
{
    public EnemyWeaponSO weapon;
    [HideInInspector] public bool isAttackComplete = true;
    [HideInInspector] public WeaponAttackAttributes currentAttackAttributes;
    protected int _attackNumber = 0;
    protected Animator _animator;
    protected Dictionary<AdditionalActionSO, AdditionalActionBase> _cachedAdditionalActions = new Dictionary<AdditionalActionSO, AdditionalActionBase>();
    protected Dictionary<int, GameObject> _cachedAttackObjects = new Dictionary<int, GameObject>();

    protected delegate void Message();
    protected Message OnFixedUpdate;
    protected Message OnDisableMoment;

    public float DamageAmount { get; set; }

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        SetWeapon();
    }

    protected virtual void FixedUpdate()
    {
        OnFixedUpdate?.Invoke();    
    }

    protected virtual void OnDisable()
    {
        OnDisableMoment?.Invoke();    
    }
    

    protected virtual void SetWeapon()
    {
        if (weapon == null)
        {
            return;
        }

        Transform whereToPut = transform;

        switch (weapon.type)
        {
            case WeaponType.Melee:
                whereToPut = transform.FirstOrDefault(obj => obj.name.Equals("MeleeAttack"));
                break;
            case WeaponType.Ranged:
                whereToPut = transform.FirstOrDefault(obj => obj.name.Equals("RangedAttack"));
                break; 
        }

        var weaponObject = Instantiate(weapon.weaponPrefab, whereToPut, false);
        weaponObject.GetComponent<SpriteRenderer>().sortingOrder = weapon.orderInLayer;

        var attacksCount = weapon.weaponAttackAttributesDictionary.Count;
        if(attacksCount > 0)
        {
            for (int i = 0; i < attacksCount; i++)
            {
                if(!_cachedAttackObjects.ContainsKey(i))
                {
                    _cachedAttackObjects.Add(
                        i, Instantiate(weapon.weaponAttackAttributesDictionary[i].attackPrefab, transform, false)
                    );
                }
            }
        }
    }

    /// <summary>
    /// Method in wich attack gets started (play animation...)
    /// </summary>
    /// <param name="props"> 1) attack number</param>
    public virtual void InitAttack(params object[] props)
    {
        if (weapon.weaponAttackAttributesDictionary.Count == 0)
        {
            return;
        }
        isAttackComplete = false;

        _attackNumber = (int)props[0];
        currentAttackAttributes = weapon.weaponAttackAttributesDictionary[_attackNumber];
    
        var attackAnimation = currentAttackAttributes.attackAnimationOverrider;
        DamageAmount = currentAttackAttributes.damageAmmount;

        if(currentAttackAttributes.additionalActions.Length > 0)
        {
            foreach(AdditionalActionSO actionSO in currentAttackAttributes.additionalActions)
            {
                if(!_cachedAdditionalActions.TryGetValue(actionSO, out var additionalAction))
                {
                    additionalAction = actionSO.CreateAdditionalAction();
                    _cachedAdditionalActions.Add(actionSO, additionalAction);

                    // Mb should to initialize each time?
                    additionalAction.Initialize(transform);
                }


                switch (actionSO.ActionPerformMoment)
                {
                    case Moment.OnUpdate:
                        OnFixedUpdate += additionalAction.PerformAction;
                        break;
                    case Moment.OnDisable:
                        OnDisableMoment += additionalAction.PerformAction;
                        break;
                    case Moment.OnEnable:
                        additionalAction.PerformAction();
                        break;
                }
            }
        }

        _animator.runtimeAnimatorController = attackAnimation;
    }

    // Called by attack animation (locates in weaponSO)
    public virtual void PerformAttack()
    {
        if(_cachedAttackObjects[_attackNumber] == null)
        {
            return;
        }
        _cachedAttackObjects[_attackNumber].transform.localPosition = currentAttackAttributes.attackPosition;
        var comp = _cachedAttackObjects[_attackNumber].GetComponent<DetectHitCollisionBase>();
        if(comp != null)
        {
            comp.Damage = currentAttackAttributes.damageAmmount;
        }

        _cachedAttackObjects[_attackNumber].SetActive(true);
    }

    // Called by attack animation (locates in weaponSO)
    public virtual void CompleteAttack()
    {
        OnDisableMoment = OnFixedUpdate = null;
        isAttackComplete = true;
        if(_cachedAttackObjects[_attackNumber] == null)
        {
            return;
        }
        _cachedAttackObjects[_attackNumber].SetActive(false);
    }
}

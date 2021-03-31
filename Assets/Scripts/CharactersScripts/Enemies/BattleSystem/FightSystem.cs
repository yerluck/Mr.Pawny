using UnityEngine;
using Pawny.AdditionalAction;
using Moment = Pawny.AdditionalAction.AdditionalActionSO.SpecificMoment;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
public class FightSystem : MonoBehaviour, IAttacker
{
    public EnemyWeaponSO weapon;
    [HideInInspector]
    public WeaponAttackAttributes currentAttackAttributes;
    protected GameObject _attackObject;
    protected Animator _animator;
    protected Dictionary<AdditionalActionSO, AdditionalActionBase> _cashedAdditionalActions = new Dictionary<AdditionalActionSO, AdditionalActionBase>();

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
                whereToPut = transform.FirstOrDefault(obj => obj.name == "MeleeAttack");
                break;
            case WeaponType.Ranged:
                whereToPut = transform.FirstOrDefault(obj => obj.name == "RangedAttack");
                break; 
        }

        var weaponObject = Instantiate(weapon.weaponPrefab, whereToPut, false);
        weaponObject.GetComponent<SpriteRenderer>().sortingOrder = weapon.orderInLayer;
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

        int attackNumber = (int)props[0];
        currentAttackAttributes = weapon.weaponAttackAttributesDictionary[attackNumber];
    
        var attackAnimation = currentAttackAttributes.attackAnimationOverrider;
        _attackObject = currentAttackAttributes.attackPrefab;
        DamageAmount = currentAttackAttributes.damageAmmount;

        if(currentAttackAttributes.additionalActions.Length > 0)
        {
            foreach(AdditionalActionSO actionSO in currentAttackAttributes.additionalActions)
            {
                if(!_cashedAdditionalActions.TryGetValue(actionSO, out var additionalAction))
                {
                    additionalAction = actionSO.CreateAdditionalAction();
                    _cashedAdditionalActions.Add(actionSO, additionalAction);

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

    public virtual void PerformAttack()
    {
        Debug.Log("Attack!!!");
    }

    public virtual void CompleteAttack()
    {
        OnDisableMoment = OnFixedUpdate = null;
    }
}

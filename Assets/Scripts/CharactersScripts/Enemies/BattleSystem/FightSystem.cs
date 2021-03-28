using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Animator))]
public class FightSystem : MonoBehaviour, IAttacker
{
    [SerializeField]
    protected EnemyWeaponSO weapon;
    [HideInInspector]
    public DictionaryAnimOverrideClip weaponClips;
    protected GameObject _attackObject;
    protected Animator _animator;

    public float DamageAmount { get; set; }

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        SetWeapon();
        weaponClips = weapon.animationOverriders;
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
        var attackAnimation = weapon.weaponAttackAttributesDictionary[attackNumber].animationOverrider;
        _attackObject = weapon.weaponAttackAttributesDictionary[attackNumber].attackPrefab;
        DamageAmount = weapon.weaponAttackAttributesDictionary[attackNumber].damageAmmount;

        _animator.runtimeAnimatorController = attackAnimation;
    }

    public virtual void PerformAttack()
    {
        Debug.Log("Attack!!!");
    }
}

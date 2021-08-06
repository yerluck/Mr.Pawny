using UnityEngine;

public abstract class FightSystem : MonoBehaviour, IFightable
{
    [SerializeField] protected EnemyWeaponSO weapon;
    [HideInInspector] public DictionaryAnimOverrideClip weaponClips;

    protected virtual void Start()
    {
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

    public abstract void Attack(int attackNum);
}

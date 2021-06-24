using UnityEngine;
using Pawny.AdditionalAction;

[CreateAssetMenu(fileName = "EnemyWeaponSO", menuName = "Weapons")]
public class EnemyWeaponSO : ScriptableObject
{
    public WeaponType type;

    public GameObject weaponPrefab;

    public int orderInLayer;

    public float attackCheckDistance;
    public float attackCheckHeight;

    public OverrideKeyAnimationDictionary handsAnimationOverriders;

    public IntWeaponAttackAttributesDictionary weaponAttackAttributesDictionary;
}

/// <summary>
/// Instance is used for attack in Fight system
/// </summary>
[System.Serializable]
public class WeaponAttackAttributes
{
    public AnimatorOverrideController attackAnimationOverrider;

    /// <summary>
    /// Object to instantiate that represents attack effect (collider, etc)
    /// </summary>
    public GameObject attackPrefab;

    public float damageAmmount;

    /// <summary>
    /// Where to instantitate attackPrefab
    /// </summary>
    public Vector3 attackPosition;

    public AdditionalActionSO[] additionalActions;
}
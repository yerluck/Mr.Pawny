using UnityEngine;

[CreateAssetMenu(fileName = "EnemyWeaponSO", menuName = "Weapons")]
public class EnemyWeaponSO : ScriptableObject
{
    public WeaponType type;

    public GameObject weaponPrefab;

    public int orderInLayer;

    public DictionaryAnimOverrideClip animationOverriders;

    public WeaponAttackAttributesDictionary weaponAttackAttributesDictionary;
}

/// <summary>
/// Instance is used for attack in Fight system
/// </summary>
[System.Serializable]
public class WeaponAttackAttributes
{
    public AnimatorOverrideController animationOverrider;
    
    /// <summary>
    /// Object to instantiate that represents attack effect (collider, etc)
    /// </summary>
    public GameObject attackPrefab;

    public float damageAmmount;
}
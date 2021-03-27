using UnityEngine;

[CreateAssetMenu(fileName = "EnemyWeaponSO", menuName = "Weapons")]
public class EnemyWeaponSO : ScriptableObject {
    public WeaponType type;

    public GameObject weaponPrefab;

    public int orderInLayer;

    public DictionaryAnimOverrideClip animationOverriders;
}

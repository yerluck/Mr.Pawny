using System;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

[Serializable]
public class OverrideKeyAnimationDictionary: SerializableDictionaryBase<EnemyAnimationOverrideKeys, AnimatorOverrideController> { }

[Serializable]
public class IntWeaponAttackAttributesDictionary: SerializableDictionaryBase<int, WeaponAttackAttributes> { }

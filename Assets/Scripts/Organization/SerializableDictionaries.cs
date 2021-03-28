using System;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

[Serializable]
public class DictionaryAnimOverrideClip: SerializableDictionaryBase<EnemyAnimationOverrideKeys, AnimatorOverrideController> { }

[Serializable]
public class WeaponAttackAttributesDictionary: SerializableDictionaryBase<int, WeaponAttackAttributes> { }

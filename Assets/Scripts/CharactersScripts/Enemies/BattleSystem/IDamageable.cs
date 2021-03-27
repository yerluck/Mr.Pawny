using UnityEngine;
using System;

public interface IDamageable {
    // event Action OnTakeDamage;

    float HP { get; set; }

    // Property - need for example get the location of attacker
    Transform Attacker { get; set; }

    void TakeDamage(float damage);

    void Die();
}
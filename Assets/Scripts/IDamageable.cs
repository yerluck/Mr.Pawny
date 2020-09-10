using UnityEngine;

public interface IDamageable {
    float HP { get; set; }

    Transform attacker { get; set; }

    void TakeDamage(float damage);
}
using UnityEngine;

public class AttackEffectCollision : MonoBehaviour
{
    public float dmg {get; set;}

    private void OnTriggerEnter2D(Collider2D other) {
        IDamageable script = other.GetComponent<IDamageable>();
        if (script != null)
        {
            script.attacker = transform;
            script.TakeDamage(dmg);
        }
    }
}

using UnityEngine;

public class AttackEffectCollision : MonoBehaviour
{
    public float dmg {get; set;}

    private void OnTriggerEnter2D(Collider2D other) {
        IDamageable damagedObject = other.GetComponent<IDamageable>();
        if (damagedObject != null)
        {
            damagedObject.attacker = transform;
            damagedObject.TakeDamage(dmg);
        }
    }
}

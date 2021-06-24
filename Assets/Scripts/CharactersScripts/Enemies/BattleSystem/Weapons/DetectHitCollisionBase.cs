using UnityEngine;

public class DetectHitCollisionBase : MonoBehaviour
{
    public float Damage { get; set; }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damagedObject = other.GetComponent<IDamageable>();

        if (damagedObject != null)
        {
            damagedObject.Attacker = transform;
            damagedObject.TakeDamage(Damage);
        }
    }
}

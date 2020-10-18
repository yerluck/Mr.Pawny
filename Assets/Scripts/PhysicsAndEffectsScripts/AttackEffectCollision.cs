using UnityEngine;

public class AttackEffectCollision : MonoBehaviour
{
    [SerializeField] private GameObject hitEffect;
    public float dmg {get; set;}

    private void OnTriggerEnter2D(Collider2D other) {
        IDamageable damagedObject = other.GetComponent<IDamageable>();
        if (damagedObject != null)
        {
            damagedObject.attacker = transform;
            damagedObject.TakeDamage(dmg);

            if (hitEffect != null)
            {
                Vector2 point = GetComponent<Collider2D>().ClosestPoint(other.transform.position);
                Instantiate(hitEffect, point, Quaternion.identity);
            }
        }
    }
}

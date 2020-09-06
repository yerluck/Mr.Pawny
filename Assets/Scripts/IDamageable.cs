public interface IDamageable {
    float HP { get; set; }

    void TakeDamage(float damage);
}
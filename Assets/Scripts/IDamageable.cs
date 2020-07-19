public interface IDamageable {
    float HP { get; set; }

    void takeDamage(float damage);
}
public interface IAttacker
{
    // damage that deals instance
    float DamageAmount { get; set; }

    //Method to prepare the attack. TODO: mb should contain only 1 paramater - attack number
    void InitAttack(params object[] props);

    void PerformAttack();
}
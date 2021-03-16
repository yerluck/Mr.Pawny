public interface IAttacker
{
    // damage that deals instance
    float DamageAmount {get; set;}

    //Method to prepare the attack
    void InitAttack(object[] props);

    void PerformAttack();
}
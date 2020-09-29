public interface IAttacker
{
    float dmgAmount {get; set;}

    void InitAttack(object[] props);

    void PerformAttack();
}
public interface LiveObject
{
    public bool IsAlive();

    public void ReceiveDamage(float damage);

    public void BeHealed(float hitpoints);
}

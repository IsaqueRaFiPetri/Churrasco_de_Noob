public interface IDamagable
{
    public void TakeDamage(float amount);
    void Die();
}

public interface ISlidable
{
    public void IncreaseSlider(float amount);
    public void DecreaseSlider(float amount);
}

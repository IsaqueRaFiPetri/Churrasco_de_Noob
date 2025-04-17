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

public interface IPowerUpable
{
    public void ChangeMoveSPD(float gain);
    public void ChangeBulletSPD(float gain);
    public void ChangeFireRate(float gain);
    public void ChangeDamage(float gain);
}

using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] float maxLife;
    float currentLife;
    [SerializeField] float gain;

    private void Start()
    {
        currentLife = maxLife;
    }

    public void TakeDamage(float amount)
    {
        currentLife -= amount;
        if(currentLife <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        SkillBar.Instance.IncreaseSlider(gain);
        Destroy(gameObject);
    }
}

using UnityEngine;

public class PowerUp : MonoBehaviour
{
    enum PowerUpType
    {
        Speed,
        BulletSpeed,
        FireRate,
        Damage
    }

    public float gain;

    [SerializeField] PowerUpType type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats stats = other.gameObject.GetComponent<PlayerStats>();
            if(stats != null)
            {
                switch (type)
                {
                    case PowerUpType.Speed:
                        stats.ChangeMoveSPD(gain);
                        break;
                    case PowerUpType.FireRate:
                        stats.ChangeFireRate(0.1f);
                        break;
                    case PowerUpType.Damage:
                        stats.ChangeDamage(gain);
                        break;
                }
            }

            Destroy(gameObject);
        }
    }
}

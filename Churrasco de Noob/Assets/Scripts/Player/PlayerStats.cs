using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float moveSpeed;
    public float bulletSpeed;
    public float fireRate;
    public float damage;

    PlayerMove move;

    private void Awake()
    {
        move = GetComponent<PlayerMove>();
    }

    public void ChangeMoveSPD(float gain)
    {
        moveSpeed += gain;
        move.speed = moveSpeed;
    }

    public void ChangeBulletSPD(float gain)
    {
        bulletSpeed += gain;
    }

    public void ChangeFireRate(float gain)
    {
        fireRate -= gain;
    }
    public void ChangeDamage(float gain)
    {
        fireRate -= gain;
    }
}

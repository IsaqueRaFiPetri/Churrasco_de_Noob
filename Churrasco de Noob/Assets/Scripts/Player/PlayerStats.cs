using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float moveSpeed;
    public float bulletSpeed;
    public float fireRate;
    public float damage;

    PlayerMove move;
    PlayerShoot shoot;

    private void Awake()
    {
        move = GetComponent<PlayerMove>();
        shoot = GetComponent<PlayerShoot>();
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
        shoot.fireRate = fireRate;
    }
    public void ChangeDamage(float gain)
    {
        damage += gain;
        shoot.damage = damage;
        Debug.Log(damage);
    }
}

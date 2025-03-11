using UnityEngine;

public class PlayerFireball : MonoBehaviour
{
    public int maxBounces = 5; // N�mero m�ximo de quicadas
    public float lifetime = 5f; // Tempo antes da bolinha desaparecer
    private int bounceCount = 0;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifetime); // Destroi a bolinha ap�s o tempo de vida
    }

    void OnCollisionEnter(Collision collision)
    {
        bounceCount++;
        if (bounceCount >= maxBounces)
        {
            Destroy(gameObject); // Destroi a bolinha ao atingir o limite de quicadas
        }
    }
}

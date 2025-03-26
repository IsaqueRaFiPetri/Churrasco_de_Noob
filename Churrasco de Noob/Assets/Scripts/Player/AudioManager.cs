using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource source;
    public AudioSource fireball;
    public AudioSource box;
    public AudioSource upgrade;
    public AudioSource explosion;

    private void Awake()
    {
        Instance = this;
    }
}

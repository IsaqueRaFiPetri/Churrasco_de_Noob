using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SkillBar.Instance.DecreaseSlider(damage);
            Collider col = GetComponent<Collider>();
            if(col != null)
            {
                col.enabled = false;
            }
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public int index;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            index += 1;
            other.gameObject.SetActive(false);
            if (index >= 2)
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
}

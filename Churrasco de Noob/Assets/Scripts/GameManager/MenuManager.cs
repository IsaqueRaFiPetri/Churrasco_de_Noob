using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public EventSystem eventSystem;
    //[SerializeField] private GameObject painelMenuInicial;

    public void Teleport(string tp)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(tp);
    }
    public void SairJogo()
    {
        Debug.Log("Sair do jogo");
        Application.Quit();
    }

    public void NextButton(GameObject button)
    {
        eventSystem.SetSelectedGameObject(button);
    }
}
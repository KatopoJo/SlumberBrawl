using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("You would quit here.");
    }
}
/*
 * This code uses parts from the CCT285 code library.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class FToRestart : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(1);
        }
    }
}
/*
 * Katopo, J.
 */

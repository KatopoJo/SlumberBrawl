using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            Debug.Log("U died");
            SceneManager.LoadScene(2);
        }
        else if (collision.transform.tag == "Exit")
        {
            Debug.Log("U win");
            SceneManager.LoadScene(3);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementMine : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Rigidbody2D rb;
    public float jumpAmount = 35;
    public float gravityScale = 10;
    public float fallingGravityScale = 40;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Moves Left and right along x Axis                               //Left/Right
        transform.Translate(Vector3.right * Time.deltaTime * Input.GetAxis("Horizontal") * moveSpeed);

        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
        }

        if (rb.velocity.y >= 0) {
            rb.gravityScale = gravityScale;
        }
        else if (rb.velocity.y < 0) {
            rb.gravityScale = fallingGravityScale;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Rigidbody2D rb;
    public float jumpAmount = 25f;
    public float gravityScale = 5f;
    public float fallingGravityScale = 12f;
    public float apexGravityScale = 3f;
    bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        // Running (holding the Shift key makes movement speed higher)
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = 15f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = 10f;
        }

        //Moves Left and right along x Axis                               //Left/Right
        transform.Translate(Vector3.right * Time.deltaTime * Input.GetAxis("Horizontal") * moveSpeed);

        if (Input.GetAxis("Horizontal") > 0)
        {
            isFacingRight = true;
        } else if (Input.GetAxis("Horizontal") < 0)
        {
            isFacingRight = false;
        }

        // Jumping (Pressing the Space or Z key adds force upwards)
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z)) {
            rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
        }

        // Dashing (Checks which direction the player is facing, then applies force)
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (isFacingRight)
            {
                rb.AddForce(Vector2.right * 10f, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(Vector2.left * 10f, ForceMode2D.Impulse);
            }
        }

        // Changes gravity scale depending on where the player is in their jump
        if (rb.velocity.y >= 1) { // Normal gravity
            rb.gravityScale = gravityScale;
            rb.drag = 2;
        }
        else if (Mathf.Abs(rb.velocity.y) < 1) // Less gravity near apex of jump
        {
            rb.gravityScale = apexGravityScale;
            rb.drag = 0;
        }
        else if (rb.velocity.y < 0) { // More gravity when falling
            rb.gravityScale = fallingGravityScale;
            rb.drag = 2;
        }

        
    }
}
// 

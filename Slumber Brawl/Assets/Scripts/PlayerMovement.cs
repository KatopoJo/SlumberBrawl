using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;
    public Rigidbody2D rb;
    public BoxCollider2D bc;
    public float moveSpeed = 10f;
    public float jumpAmount = 15f;
    public float buttonTime = 0.2f;
    float jumpTime = 0;
    bool isJumping = false;
    bool isFacingRight = true;
    public float gravityScale = 6f;
    public float fallingGravityScale = 12f;
    public float apexGravityScale = 3f;

    // Start is called before the first frame update
    void Start()
    {
        // Freezes rotate so that the player always stays upright
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Run (Holding the Shift key makes movement speed higher)
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = 15f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = 10f;
        }

        // Move left and right
        transform.Translate(Vector3.right * Time.deltaTime * Input.GetAxis("Horizontal") * moveSpeed);

        // Jump (Pressing the Space or Z key adds force upwards)
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z)) && IsGrounded())
        {
            rb.AddForce(Vector2.up * (jumpAmount * 0.8f), ForceMode2D.Impulse);
            isJumping = true;
            jumpTime = 0;
        }
        if (isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpAmount);
            jumpTime += Time.deltaTime;
        }
        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Z)) | jumpTime > buttonTime)
        {
            isJumping = false;
        }

        // Checks which way the player is facing using GetAxis, note 0 is left out so that it will
        // stick to the last state before pressing no direction
        if (Input.GetAxis("Horizontal") > 0)
        {
            isFacingRight = true;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            isFacingRight = false;
        }

        // Dash (Checks which direction the player is facing, then applies force)
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
        if (rb.velocity.y >= 1) // Normal gravity
        {
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
    
    private bool IsGrounded()
    {
        float extraDistance = 1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, extraDistance, platformLayerMask);
        return raycastHit.collider != null;
    }
}
/*
 * Shows several ways to implement different types of jumps
 * Also shows how to implement variable jump height
 * https://gamedevbeginner.com/how-to-jump-in-unity-with-or-without-physics/
 * 
 * Showcases common mechanics for player movement in good 2D platformers
 * http://www.davetech.co.uk/gamedevplatformer
 * 
 * Documentation for the drag property in Rigidbody2D components
 * https://docs.unity3d.com/ScriptReference/Rigidbody2D-drag.html
 * 
 * Documentation for the GetAxis method under the Input class
 * https://docs.unity3d.com/ScriptReference/Input.GetAxis.html
 * 
 * How to check if the player is touching the ground
 * https://www.youtube.com/watch?v=c3iEl5AwUF8
 */

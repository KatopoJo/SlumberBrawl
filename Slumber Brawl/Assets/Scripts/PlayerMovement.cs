using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;
    public Rigidbody2D rigidbody2d;
    public BoxCollider2D boxcollider2d;
    public Animator animator;
    public AudioSource[] speaker;
    public SpriteRenderer sprite;
    public float moveSpeed = 15f;
    public float jumpAmount = 20f;
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
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Run (Holding the Shift key makes movement speed higher)
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = 20f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = 15f;
        }

        // Move left and right
        transform.Translate(Vector3.right * Time.deltaTime * Input.GetAxis("Horizontal") * moveSpeed);
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));

        // Jump (Pressing the Space or Z key adds force upwards)
        animator.SetBool("Jumping", isJumping);
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z)) && IsGrounded())
        {
            rigidbody2d.AddForce(Vector2.up * (jumpAmount * 0.8f), ForceMode2D.Impulse);
            isJumping = true;
            jumpTime = 0;
            speaker[0].Play();
        }
        if (isJumping)
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpAmount);
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
            sprite.flipX = false;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            isFacingRight = false;
            sprite.flipX = true;
        }

        // Dash (Checks which direction the player is facing, then applies force)
        if (Input.GetKeyDown(KeyCode.X))
        {
            speaker[1].Play();
            if (isFacingRight)
            {
                rigidbody2d.AddForce(Vector2.right * 15f, ForceMode2D.Impulse);
            }
            else
            {
                rigidbody2d.AddForce(Vector2.left * 15f, ForceMode2D.Impulse);
            }
            animator.SetBool("Dash", true);
        }
        else
        {
            animator.SetBool("Dash", false);
        }

        // Changes gravity scale depending on where the player is in their jump
        if (rigidbody2d.velocity.y >= 1) // Normal gravity
        {
            rigidbody2d.gravityScale = gravityScale;
            rigidbody2d.drag = 2;
        }
        else if (Mathf.Abs(rigidbody2d.velocity.y) < 1) // Less gravity near apex of jump
        {
            rigidbody2d.gravityScale = apexGravityScale;
            rigidbody2d.drag = 0;
        }
        else if (rigidbody2d.velocity.y < 0) { // More gravity when falling
            rigidbody2d.gravityScale = fallingGravityScale;
            rigidbody2d.drag = 2;
        }
    }
    
    private bool IsGrounded()
    {
        float extraDistance = 1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxcollider2d.bounds.center, boxcollider2d.bounds.size, 0f, Vector2.down, extraDistance, platformLayerMask);
        return raycastHit.collider != null;
    }
}
/*
 * Line 44 of this program was taken from the SmoothCompassMovement2D script
 * in the CCT423 code library.
 * How to add movement in a 2D game:
 * https://forum.unity.com/threads/move-left-right-up-down-script-for-noobs-c.168848/
 * 
 * Shows several ways to implement different types of jumps.
 * Also shows how to implement variable jump height:
 * https://gamedevbeginner.com/how-to-jump-in-unity-with-or-without-physics/
 * 
 * Common mechanics for player movement in good 2D platformers:
 * http://www.davetech.co.uk/gamedevplatformer
 * 
 * Documentation for the drag property in Rigidbody2D components:
 * https://docs.unity3d.com/ScriptReference/Rigidbody2D-drag.html
 * 
 * Documentation for the GetAxis method under the Input class:
 * https://docs.unity3d.com/ScriptReference/Input.GetAxis.html
 * 
 * How to check if the player is touching the ground:
 * https://www.youtube.com/watch?v=c3iEl5AwUF8
 * 
 * Parts of PlaySoundsOnKeyPress.cs from the CCT423 code library were used.
 * How to play multiple audio sources using button presses:
 * https://frederikmax.com/?p=912
 * 
 * How to add player animations:
 * https://youtu.be/hkaysu1Z-N8
 * 
 * How to flip a sprite based on direction:
 * https://vionixstudio.com/2022/03/30/how-to-flip-a-sprite-in-unity/#:~:text=FlipX%20flips%20the%20sprite%20along,FlipY%20checkbox%20on%20the%20SpriteRenderer.
 */

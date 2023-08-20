using UnityEngine;

public class BasicChase2D : MonoBehaviour
{
    public GameObject destination;
    public Rigidbody2D rigidbody2d;
    public SpriteRenderer sprite;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = destination.transform.position - transform.position;
        transform.position = Vector2.MoveTowards(this.transform.position, destination.transform.position, speed * Time.deltaTime);

        // note 0 is left out so that it will stick to the last state
        if (direction.x < 0)
        {
            sprite.flipX = true;
        }
        else if (direction.x > 0)
        {
            sprite.flipX = false;
        }
    }
}
/*
 * This code was taken from the CCT423 code library and modified.
 * How to chase a game object in 2D:
 * https://www.youtube.com/watch?v=2SXa10ILJms
 * 
 * How to flip a sprite based on direction:
 * https://vionixstudio.com/2022/03/30/how-to-flip-a-sprite-in-unity/#:~:text=FlipX%20flips%20the%20sprite%20along,FlipY%20checkbox%20on%20the%20SpriteRenderer.
 */

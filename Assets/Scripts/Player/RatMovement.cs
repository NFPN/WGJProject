using UnityEngine;

public class RatMovement : MonoBehaviour
{
    public float speed;

    private Vector2 moveDirection;
    private Rigidbody2D rb;
    private float vertical;
    private float horizontal;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        moveDirection = rb.velocity;
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void FixedUpdate()
    {
        if (horizontal < 0.2f && horizontal > -0.2f)
            rb.velocity = new Vector2(0, rb.velocity.y);
        else
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (vertical < 0.2f && vertical > -0.2f)
            rb.velocity = new Vector2(rb.velocity.x, 0);
        else
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
    }
}

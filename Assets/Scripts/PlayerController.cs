using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // "Public" variables
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float jumpForce = 700.0f;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private LayerMask whatIsGround;

    // Private variables
    private Rigidbody2D rBody;
    private bool isGrounded = false;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        // For testing
        //float horiz = Input.GetAxis("Horizontal");
        //rBody.velocity = new Vector2(horiz * speed, rBody.velocity.y);


        isGrounded = GroundCheck();
        rBody.velocity = new Vector2(speed, rBody.velocity.y);



        // Jump code
        if (isGrounded && Input.GetAxis("Jump") > 0)
        {
            if (rBody.gravityScale < 0)
            {
                rBody.AddForce(new Vector2(0.0f, -jumpForce));
                isGrounded = false;
            }
            if (rBody.gravityScale > 0)
            {
                rBody.AddForce(new Vector2(0.0f, jumpForce));
                isGrounded = false;
            }            
        }
    }

    private bool GroundCheck()
    {
        return Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, whatIsGround);
    }

    public void Flip()
    {
        Vector3 temp = transform.localScale;
        temp.y *= -1;
        transform.localScale = temp;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // "Kill" player if they touch a spike
        if (other.gameObject.CompareTag("Spike"))
        {
            Debug.Log(timer);
            timer = 0;
            this.transform.position = new Vector3(-10.27f, -4.4f, 0.0f);
            if (rBody.gravityScale < 0)
            {
                rBody.gravityScale *= -1;
                Flip();
            }
        }
    }
}

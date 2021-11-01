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
    [SerializeField] private GameObject deathSoundContainer;

    // Private variables
    private Rigidbody2D rBody;
    private Animator anim;
    private bool isGrounded = false;
    private bool isDead = false;
    AudioSource jumpSound;
    AudioSource deathSound;


    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpSound = GetComponent<AudioSource>();
        deathSound = deathSoundContainer.GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

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
                jumpSound.Play();
            }
            if (rBody.gravityScale > 0)
            {
                rBody.AddForce(new Vector2(0.0f, jumpForce));
                isGrounded = false;
                jumpSound.Play();
            }            
        }

        // Communicate with the animator
        anim.SetFloat("xVelocity", Mathf.Abs(rBody.velocity.x));
        anim.SetFloat("yVelocity", rBody.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isDead", isDead);
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
            isDead = true;
            rBody.constraints = RigidbodyConstraints2D.FreezeAll;
            deathSound.Play();
            //this.transform.position = new Vector3(-10.27f, -4.4f, 0.0f);
            //if (rBody.gravityScale < 0)
            //{
            //    rBody.gravityScale *= -1;
            //    Flip();
            //}

            
        }
    }
}

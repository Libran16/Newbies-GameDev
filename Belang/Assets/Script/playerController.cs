using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    //movement variable
    public float runSpeed; //run
    public float speedRun; //speedrun
    public float fall;
    Rigidbody rbChar;
    Animator animChar;
    bool facingRight;

    // Jump
    bool grounded = false;
    Collider[] groundCollisions;
    float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHeight;

    // Start is called before the first frame update
    void Start()
    {
        rbChar = GetComponent<Rigidbody>();
        animChar = GetComponent<Animator>();
        facingRight = true;
    }

    // Update is called once per frame, faster/slower
    void Update()
    {
        
    }
    
    // is called fix after physic run
    void FixedUpdate()
    {
        if(grounded && Input.GetAxis("Jump") > 0)
        {
            grounded = false;
            animChar.SetFloat("verticalSpeed", fall);
            animChar.SetBool("grounded", grounded);
            rbChar.AddForce(new Vector3(0,jumpHeight,0));
        }

        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if(groundCollisions.Length > 0) grounded = true;
        else grounded = false;

        animChar.SetBool("grounded", grounded);

        float move = Input.GetAxis("Horizontal");
        animChar.SetFloat("speed",Mathf.Abs(move));

        float speedrun = Input.GetAxisRaw("Fire3");
        animChar.SetFloat("speedrun",speedrun);

        if(speedrun > 0 )
        {
            rbChar.velocity = new Vector3(move*speedRun, rbChar.velocity.y, 0);
        }
        else
        {
            rbChar.velocity = new Vector3(move*runSpeed, rbChar.velocity.y, 0);
        }

        if(move > 0 && !facingRight) Flip();
        else if (move < 0 && facingRight) Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 thScale = transform.localScale;
        thScale.z *= -1;
        transform.localScale = thScale;
    }
}

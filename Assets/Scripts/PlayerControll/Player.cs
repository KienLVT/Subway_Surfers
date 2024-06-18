using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 0f;
    private Rigidbody player;
    [SerializeField] private Vector3 direction = Vector3.zero;
    private bool isGrounded = false;
    [SerializeField] private float laneWidth = 2f;
    [SerializeField] public float jumpForce = 0f;

    private float y;

    private float playerX;
    [SerializeField] private Vector3 velocity= Vector3.zero;
    [SerializeField] private float SmoothTime = 0f;
    [SerializeField] private GameObject Limiter;
    [SerializeField] private float groundCheckDistance;

    [SerializeField] private Animator animator;
    private CapsuleCollider capsuleCollider;
    private Vector3 originalCenter;
    private float originRad;
    private float originHeight;
    private bool isSliding = false;

    [SerializeField] private float downForce = 1f;

    public bool isJump = false;

    private void Start()
    {
        player = GetComponent<Rigidbody>();
        Limiter = GameObject.Find("Limiter");

        capsuleCollider = GetComponent<CapsuleCollider>();
        originalCenter = capsuleCollider.center;
        originRad = capsuleCollider.radius;
        originHeight = capsuleCollider.height;
        animator = GetComponent<Animator>();
        
    }

    private void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
            
            
        }
        
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

        if(Input.GetKeyDown(KeyCode.DownArrow) && !isSliding)
        {
            if(isGrounded)
            {
                StartCoroutine(Slide());
            }
            else
            {
                JumpDown();
            }
        }
    

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            playerX -= laneWidth;
            animator.Play("LeftTurn");
            
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerX += laneWidth;
            animator.Play("RightTurn");
            
        }

        Vector3 targetLane = new Vector3 (playerX, transform.position.y, transform.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, targetLane, ref velocity, SmoothTime);
        transform.forward = Vector3.forward;

        float minX = Limiter.transform.position.x - Limiter.transform.localScale.x/2;
        float maxX = Limiter.transform.position.x + Limiter.transform.localScale.x / 2;
        playerX = Mathf.Clamp(playerX, minX, maxX);
    }

    private void Jump()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Falling"))
        {
            animator.Play("Landing");
            isJump = false;
            return;
        }
      
        if(isGrounded )
        {
            player.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;

            y = jumpForce;
            animator.CrossFadeInFixedTime("isJumping", 0.01f);
            isJump = true;

            
        }
        else
        {
            y-= jumpForce*2*Time.deltaTime;
            if(player.velocity.y<0.1f)
            animator.Play("Falling");
        }
       
       
        
    }
    private void JumpDown()
    {
        player.AddForce(Vector3.down* downForce, ForceMode.Impulse);
        animator.Play("FallRoll");
    }
    private void FixedUpdate()
    {
        Vector3 rbvelocity = player.velocity;

        rbvelocity.z = direction.z * speed;
        player.velocity = rbvelocity;
    }

    private IEnumerator Slide()
    {
        animator.Play("Sliding");
        isSliding = true;
        capsuleCollider.center = new Vector3(0, -0.6f, 0);
        capsuleCollider.radius = 0.3f;
        capsuleCollider.height = 1f;

        yield return new WaitForSeconds(1.3f);

        capsuleCollider.center = originalCenter;
        capsuleCollider.radius = originRad;
        capsuleCollider.height = originHeight;
        isSliding = false;

        
    }
   
}

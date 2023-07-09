using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float forceOfJump;
    
    [SerializeField] private float groundDrag;
    [SerializeField] private float playerHeight;
    [SerializeField] private float playerWidth;
    [SerializeField] private LayerMask whatIsGround;

    private PlayerContacter playerContacter;

    private Animator animator;
    
    private Rigidbody rb;

    private float timeOnStun;
    
    private bool isGrounded;
    
    // Start is called before the first frame update
    void Start()
    {
        timeOnStun = -1;
        rb = this.GetComponent<Rigidbody>();
        playerContacter = this.GetComponent<PlayerContacter>();
        
        rb.freezeRotation = true;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -40) SceneManager.LoadScene("Scenes/SampleScene");
        if (timeOnStun > 0)
        {
            timeOnStun -= Time.deltaTime;
            return;
        }
        isGrounded = IsOnGround();
        
        HandleMovement();

        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void HandleMovement()
    {   
        float moveX = 0f;
        float moveZ = 0f;
        float moveY = 0f;
        if (Input.GetKey(KeyCode.W))
        {
            moveX = 1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveX = -1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveZ = 1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveZ = -1f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveY = 1f;
        }
        
        Vector3 direction = new Vector3(moveX, 0, moveZ);
        if (isGrounded)
        {
            animator.SetFloat("Speed", direction.sqrMagnitude);
            rb.AddForce(direction.normalized * speed, ForceMode.Force);
            transform.rotation = Quaternion.Slerp(transform.rotation,  Quaternion.LookRotation(direction.normalized), 1);
        }
        else
        {   
            animator.SetFloat("Speed", 0);
            rb.AddForce(direction.normalized * speed / 2, ForceMode.Force);
        }
        

        //rotate us over time according to speed until we are in the required rotation
        if (moveY != 0 && isGrounded) rb.AddForce(Vector3.up * forceOfJump * 100f, ForceMode.Force);
        
    }
    
    
    
    public bool IsOnGround()
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.SphereCast(transform.position, playerWidth / 2, Vector3.down, out hit, playerHeight / 2, whatIsGround))
        {
            return true;
        };
        return false;
    }

    public void Stun(float time)
    {
        timeOnStun = time;
    }
}

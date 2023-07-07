using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float forceOfJump;
    
    [SerializeField] private float groundDrag;
    [SerializeField] private float playerHeight;
    [SerializeField] private float playerWidth;
    [SerializeField] private LayerMask whatIsGround;

    private PlayerContacter playerContacter;
    
    private Rigidbody rb;
    private bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        playerContacter = this.GetComponent<PlayerContacter>();
        
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = isOnGround();
        
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
            rb.AddForce(direction.normalized * speed, ForceMode.Force);
            transform.rotation = Quaternion.Slerp(transform.rotation,  Quaternion.LookRotation(direction.normalized), 1);
        }


        //rotate us over time according to speed until we are in the required rotation
        if (moveY != 0 && isGrounded) rb.AddForce(Vector3.up * forceOfJump * 100f, ForceMode.Force);
        
    }
    
    
    
    public bool isOnGround()
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.SphereCast(transform.position, playerWidth / 2, Vector3.down, out hit, playerHeight / 2, whatIsGround))
        {
            return true;
        };
        return false;
    }
    
    
}

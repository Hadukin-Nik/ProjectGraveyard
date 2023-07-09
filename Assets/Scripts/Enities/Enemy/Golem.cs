using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float golemWidth;
    [SerializeField] private float distanceReaction;
    [SerializeField] private float timeToRun;
    [SerializeField] private float maxDistance;

    [SerializeField] private LayerMask whatIsGround; 
    
    private PlayerMovement pw;

    private Animator animator;

    private Vector3 destination;

    private float time;

    private Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        pw = (PlayerMovement)GameObject.FindObjectsOfType(typeof(PlayerMovement))[0];
        position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

        animator = GetComponent<Animator>();
        
        time = timeToRun;
    }

    // Update is called once per frame
    void Update()
    {
        if (pw.IsOnGround() && destination.sqrMagnitude <= 0.001  && canIRun())
        {
            destination = (pw.transform.position - transform.position) * speed;
        }

        if (destination.sqrMagnitude > 0.001 && time <= 0)
        {
            animator.SetFloat("Speed", speed);
            transform.position += destination * Time.deltaTime;
        } 
    }

    public void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.tag.Equals("BreakableWall"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private bool canIRun()
    {
        RaycastHit hit = new RaycastHit();
        
        
        if (Physics.SphereCast(position, golemWidth/2, transform.forward, out hit, maxDistance))
        {
            if (time > 0) time -= Time.deltaTime;
            return time <= 0;
        };
        time = timeToRun;
        return false;
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color=Color.red;
        Gizmos.DrawSphere(position+transform.forward*maxDistance,golemWidth);
    }
}

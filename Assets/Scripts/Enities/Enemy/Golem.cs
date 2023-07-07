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
    
    private PlayerMovement pw;

    private Vector3 destination;

    private float time;
    

    // Start is called before the first frame update
    void Start()
    {
        pw = (PlayerMovement)GameObject.FindObjectsOfType(typeof(PlayerMovement))[0];
        time = timeToRun;
    }

    // Update is called once per frame
    void Update()
    {
        if (pw.isOnGround() && canIRun() && destination.sqrMagnitude <= 0.001)
        {
            destination = (pw.transform.position - transform.position) * speed;
        }

        if (destination.sqrMagnitude > 0.001 && time <= 0 )
        {
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
        if (Physics.SphereCast(transform.position, golemWidth / 2, Vector3.forward, out hit))
        {
            if (time > 0) time -= Time.deltaTime;
            return time <= 0;
        };
        time = timeToRun;
        return false;
    }
}

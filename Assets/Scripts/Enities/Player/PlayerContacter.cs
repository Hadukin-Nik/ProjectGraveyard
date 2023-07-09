using System;
using System.Collections;
using System.Collections.Generic;
using Imterfaces;
using Objects;
using UnityEngine;

public class PlayerContacter : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float timeStun;
    
    private PlayerMovement pm;
    private bool haveKey;

    public void Start()
    {
        pm = gameObject.GetComponent<PlayerMovement>();
    }

    public void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            
            case "Trap":
                ITrap trap = other.gameObject.GetComponent<Trap>();
                trap.TrapAction();
                break;
            
            
            default:
                transform.SetParent(other.transform);
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.tag)
        {
            default:
                transform.SetParent(null);
                break;
        }    }

    public void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Key":
                haveKey = true;
                other.gameObject.SetActive(false);
                break;
            case "Golem":
                pm.Stun(timeStun);
                break;
        }
    }
    
    

    public bool HaveAKey()
    {
        return haveKey;
    }
    
}

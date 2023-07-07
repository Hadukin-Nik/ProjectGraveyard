using System;
using System.Collections;
using System.Collections.Generic;
using Imterfaces;
using Objects;
using UnityEngine;

public class PlayerContacter : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsGround;

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
            case "Collectable":
                if (other.gameObject.name.Contains("key"))
                {
                    haveKey = true;
                    other.gameObject.SetActive(false);
                }
                break;
            case "Trap":
                ITrap trap = other.gameObject.GetComponent<Trap>();
                trap.TrapAction();
                break;
            case "Enemy":
                pm.
                
        }
    }
    
}

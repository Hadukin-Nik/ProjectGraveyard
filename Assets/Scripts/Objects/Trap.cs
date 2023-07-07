using System;
using System.Threading;
using Imterfaces;
using UnityEngine;

namespace Objects
{
    public class Trap : MonoBehaviour, ITrap
    {
        [SerializeField] private float maxTime;
        [SerializeField] private Platform[] platforms;
        
        [SerializeField] private bool isMovable = true;
        [SerializeField] private Transform connectedTo;
        [SerializeField] private bool neededAKey;

        private PlayerContacter pc;
        
        private Vector3 originalDistToSourcePlatfrom;

        private float timer;
        private bool activated;
        public void Start()
        {
            pc = (PlayerContacter)GameObject.FindObjectsOfType(typeof(PlayerContacter))[0];
            timer = maxTime;
            if(isMovable)
            originalDistToSourcePlatfrom = connectedTo.position - this.transform.position;
        }

        public void TrapAction()
        {
            if(!neededAKey || neededAKey && pc.HaveAKey()) moveAllPlatforms(true);
        }

        public void Update()
        {
            if(isMovable) transform.position = connectedTo.position - originalDistToSourcePlatfrom;
            
            if (activated)
            {
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    moveAllPlatforms(false);
                    timer = maxTime;
                }
            }
        }

        private void moveAllPlatforms(bool activatedState)
        {
            foreach (var i in platforms)
            {
                i.Move();
            }

            activated = activatedState;
        }
    }
}
using System;
using UnityEngine;

namespace Objects
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private Vector3 moveTo;
        [SerializeField] private float timeOnMove;
        [SerializeField] private bool workAI = true;

        private Vector3 sourcePosition;
        private bool moveToMoveTo;
        private bool commandMove;
        
        public void turnAI(bool value)
        {
            workAI = value;
        }

        public void Start()
        {
            sourcePosition = this.transform.position;
            moveToMoveTo = true;
        }

        public void Update()
        {
            if (workAI || commandMove)
            {
                if ((transform.position - moveTo).sqrMagnitude < 0.001 && moveToMoveTo ||
                    (transform.position - sourcePosition).sqrMagnitude < 0.001 && !moveToMoveTo)
                {
                    moveToMoveTo = !moveToMoveTo;
                    commandMove = false;
                }
                if (moveToMoveTo)
                {
                    transform.position += (moveTo - sourcePosition) / timeOnMove * Time.deltaTime;
                }
                else
                {
                    transform.position -= (moveTo - sourcePosition) / timeOnMove * Time.deltaTime;
                }
            }   
        }

        public void Move()
        {
            commandMove = true;
        }
    }
}
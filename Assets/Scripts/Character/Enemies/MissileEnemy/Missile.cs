using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RVT
{
    public class Missile : CharacterProjectile
    {
        private Transform target;
        [SerializeField] private Transform missileBody;
        [SerializeField] private float angle; //TEMP TEMP TEMP TEMP TEMP

        private bool fire; //TEMP TEMP TEMP TEMP TEMP
        Quaternion rot; //TEMP TEMP TEMP TEMP TEMP

        void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            Invoke("HandleMissile", angle);
        }

        void FixedUpdate()
        {
            if (fire) //TEMP TEMP TEMP TEMP TEMP
            {
                HandleMissileRotationAndPosition();
            }     
        }

        public void HandleMissile()
        {
            fire = true;
        }

        private void HandleMissileRotationAndPosition()
        {
            transform.position = Vector3.Slerp(transform.position, target.position, 1.8f * Time.deltaTime);
            missileBody.LookAt(target.forward, transform.right);
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);
            missileBody.gameObject.SetActive(false);
        }
    }
}

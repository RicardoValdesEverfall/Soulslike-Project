using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RVT
{
    public class Missile : CharacterProjectile
    {
        private Transform target;
        [SerializeField] private Transform missileBody;

        // Start is called before the first frame update
        void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            HandleMissileRotationAndPosition();
        }

        public IEnumerator HandleMissile()
        {
           

            yield return null;
        }

        private void HandleMissileRotationAndPosition()
        {
            transform.position = Vector3.Slerp(transform.position, target.position, 0.5f * Time.deltaTime);
            missileBody.LookAt(target.forward, transform.right);
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);
        }
    }
}

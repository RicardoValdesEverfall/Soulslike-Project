using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RVT
{
    public class CharacterProjectile : MonoBehaviour
    {
        [SerializeField] public float VFXDespawnTime = 5f;

        private GameObject ImpactVFX;
        private CharacterManager Character;
        private Transform Origin;
        private Transform Target;

        protected virtual void Awake()
        {
            Origin = GetComponentInParent<Transform>();
        }

        public virtual void SetTarget(Transform _target)
        {
            Target = _target;
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Player"))
            {
                //WE HIT PLAYER!!!
            }

            else if (collision.transform.CompareTag("AI"))
            {
                //WE HIT ENEMY!!!
            }

            GameObject vfx = Instantiate(ImpactVFX, collision.transform.position, transform.rotation, transform);
            Destroy(vfx, VFXDespawnTime);
        }
    }
}

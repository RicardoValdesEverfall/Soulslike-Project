using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RVT
{
    public class CharacterProjectile : MonoBehaviour
    {
        [SerializeField] public float VFXDespawnTime;
        [SerializeField] private int MyID;
        [SerializeField] private ProjectilesSO ProjectilesDB;

        private GameObject ImpactVFX;
        private Transform Origin;
        private CharacterManager Target;

        protected virtual void Awake()
        {
            Origin = GetComponentInParent<Transform>();

            ImpactVFX = ProjectilesDB.ProjectilesData[MyID].VFXImpact;
            VFXDespawnTime = ProjectilesDB.ProjectilesData[MyID].DespawnTime;
        }

        public virtual void SetTarget(CharacterManager _target)
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
            this.GetComponent<CapsuleCollider>().enabled = false;
        }
    }
}

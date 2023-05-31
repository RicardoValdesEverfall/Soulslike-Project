using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RVT
{
    public class CharacterAnimatorManager : MonoBehaviour
    {
        private CharacterManager Character;

        private float horizontal;
        private float vertical;

        protected virtual void Awake()
        {
            Character = GetComponent<CharacterManager>();
        }

        public void UpdateAnimatorMovementParameters(float horizontal, float vertical)
        {
            Character.animator.SetFloat("Horizontal", horizontal, 0.1f, Time.deltaTime);
            Character.animator.SetFloat("Vertical", vertical, 0.1f, Time.deltaTime);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

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

        public virtual void PlayActionAnimation
            (string targetAnim, 
            bool isPerformingAction, 
            bool rootMotion,
            bool canRotate = false, 
            bool canMove = false)
        {
            Character.animator.CrossFade(targetAnim, 0.2f);
            Character.PerformingAction = isPerformingAction;
            Character.ApplyRootMotion = rootMotion;
            Character.CanRotate = canRotate;
            Character.CanMove = canMove;

            Character.CharacterNetworkManagerComponent.NotifyServerOfAnimationActionServerRpc(NetworkManager.Singleton.LocalClientId, targetAnim, rootMotion);
        }
    }
}

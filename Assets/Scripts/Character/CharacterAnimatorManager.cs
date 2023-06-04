using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace RVT
{
    public class CharacterAnimatorManager : MonoBehaviour
    {
        private CharacterManager Character;

        private int horizontalAmount;
        private int verticalAmount;

        protected virtual void Awake()
        {
            Character = GetComponent<CharacterManager>();

            horizontalAmount = Animator.StringToHash("Horizontal");
            verticalAmount = Animator.StringToHash("Vertical");
        }

        public void UpdateAnimatorMovementParameters(float horizontal, float vertical, bool sprint)
        {
            float h = horizontal;
            float v = vertical;

            if (sprint)
            {
                v = 2;
            }

            Character.animator.SetFloat(horizontalAmount, h, 0.1f, Time.deltaTime);
            Character.animator.SetFloat(verticalAmount, v, 0.1f, Time.deltaTime);
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

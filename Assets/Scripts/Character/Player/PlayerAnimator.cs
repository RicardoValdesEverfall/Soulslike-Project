using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RVT
{
    public class PlayerAnimator : CharacterAnimatorManager
    {
        PlayerManager player;

        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
        }

        private void OnAnimatorMove()
        {
            if (player.ApplyRootMotion)
            {
                Vector3 velocity = player.animator.deltaPosition;
                player.CharacterControllerComponent.Move(velocity);
                player.transform.rotation *= player.animator.deltaRotation;
            }
        }
    }
}

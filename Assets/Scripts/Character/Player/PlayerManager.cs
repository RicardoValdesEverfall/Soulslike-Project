using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RVT
{
    public class PlayerManager : CharacterManager
    {
        private PlayerLocomotion _playerLocomotionManager;

        protected override void Awake()
        {
            base.Awake();

            _playerLocomotionManager = GetComponent<PlayerLocomotion>();
        }

        protected override void Update()
        {
            base.Update();

            if (!IsOwner) { return; }
            _playerLocomotionManager.HandleAllMovement();
        }
    }
}


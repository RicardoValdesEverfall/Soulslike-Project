using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RVT
{
    public class PlayerManager : CharacterManager
    {
        [HideInInspector] public PlayerLocomotion _playerLocomotionManager;
        [HideInInspector] public PlayerAnimator _playerAnimator;

        protected override void Awake()
        {
            base.Awake();

            _playerLocomotionManager = GetComponent<PlayerLocomotion>();
            _playerAnimator = GetComponent<PlayerAnimator>();
        }

        protected override void Update()
        {
            base.Update();

            if (!IsOwner) { return; }
            _playerLocomotionManager.HandleAllMovement();
        }

        protected override void LateUpdate()
        {
            if (!IsOwner) { return; }
            base.LateUpdate();

            PlayerCamera.Instance.HandleAllCameraActions();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkDespawn();

            if (IsOwner)
            {
                PlayerCamera.Instance.Player = this;
                PlayerInputManager.Instance.Player = this;
            }
        }
    }
}


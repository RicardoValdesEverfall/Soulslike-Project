using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RVT
{
    public class PlayerLocomotion : CharacterLocomotionManager
    {
        private PlayerManager _playerManager;

        [SerializeField] private float WalkingSpeed = 2;
        [SerializeField] private float RunningSpeed = 5;
        [SerializeField] private float RotationSpeed = 15;

        [HideInInspector] public float _verticalMovement;
        [HideInInspector] public float _horizontalMovement;
        [HideInInspector] public float _moveAmount;

        [Header("DODGE SETTINGS")]
        private Vector3 RollDirection;

        private Vector3 MoveDirection;
        private Vector3 TargetRotationDirection;

        protected override void Awake()
        {
            base.Awake();

            _playerManager = GetComponent<PlayerManager>();
        }

        protected override void Update()
        {
            base.Update();

            if (_playerManager.IsOwner)
            {
                _playerManager.CharacterNetworkManagerComponent.animatorHorizontalParameter.Value = _horizontalMovement;
                _playerManager.CharacterNetworkManagerComponent.animatorVerticalParameter.Value = _verticalMovement;
                _playerManager.CharacterNetworkManagerComponent.animatorMoveParameter.Value = _moveAmount;
            }

            else
            {
                _horizontalMovement = _playerManager.CharacterNetworkManagerComponent.animatorHorizontalParameter.Value;
                _verticalMovement = _playerManager.CharacterNetworkManagerComponent.animatorVerticalParameter.Value;
                _moveAmount = _playerManager.CharacterNetworkManagerComponent.animatorMoveParameter.Value;

                _playerManager._playerAnimator.UpdateAnimatorMovementParameters(0, _moveAmount);
            }
        }

        public void HandleAllMovement()
        {
            HandleGroundedMovement();
            HandleRotation();
        }

        private void HandleGroundedMovement()
        {
            GetMovementValues();

            if (!_playerManager.CanMove) { return; }

            MoveDirection = PlayerCamera.Instance.transform.forward * _verticalMovement;
            MoveDirection = MoveDirection + PlayerCamera.Instance.transform.right * _horizontalMovement;
            MoveDirection.Normalize();
            MoveDirection.y = 0;

            if (PlayerInputManager.Instance.MoveAmount > 0.5f)
            {
                _playerManager.CharacterControllerComponent.Move(MoveDirection * RunningSpeed * Time.deltaTime);
            }
            else if (PlayerInputManager.Instance.MoveAmount <= 0.5f)
            {
                _playerManager.CharacterControllerComponent.Move(MoveDirection * WalkingSpeed * Time.deltaTime);
            }
        }

        private void HandleRotation()
        {
            if (!_playerManager.CanRotate) { return; }

            TargetRotationDirection = Vector3.zero;
            TargetRotationDirection = PlayerCamera.Instance.CameraObj.transform.forward * _verticalMovement;
            TargetRotationDirection = TargetRotationDirection + PlayerCamera.Instance.CameraObj.transform.right * _horizontalMovement;
            TargetRotationDirection.Normalize();
            TargetRotationDirection.y = 0;

            if (TargetRotationDirection == Vector3.zero) { TargetRotationDirection = transform.forward; }

            Quaternion newRot = Quaternion.LookRotation(TargetRotationDirection);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRot, RotationSpeed * Time.deltaTime);
            transform.rotation = targetRotation;
        }

        private void GetMovementValues()
        {
            _verticalMovement = PlayerInputManager.Instance.VerticalInput;
            _horizontalMovement = PlayerInputManager.Instance.HorizontalInput;
            _moveAmount = PlayerInputManager.Instance.MoveAmount;
            //CLAMP MOVEMENTS
        }

        public void AttemptToPerformDodge()
        {
            if (_playerManager.PerformingAction) { return; }

            if (_moveAmount > 0)
            {
                RollDirection = PlayerCamera.Instance.CameraObj.transform.forward * _verticalMovement;
                RollDirection += PlayerCamera.Instance.CameraObj.transform.right * _horizontalMovement;
                RollDirection.y = 0;
                RollDirection.Normalize();

                Quaternion playerRot = Quaternion.LookRotation(RollDirection);
                _playerManager.transform.rotation = playerRot;

                _playerManager._playerAnimator.PlayActionAnimation("Roll_Forward", true, true);
            }
            else
            {
                _playerManager._playerAnimator.PlayActionAnimation("Roll_Back", true, true);
            }
        }
    }
}

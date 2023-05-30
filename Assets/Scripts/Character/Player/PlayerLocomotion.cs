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

        public float _verticalMovement;
        public float _horizontalMovement;
        public float _moveAmount;

        private Vector3 MoveDirection;
        private Vector3 TargetRotationDirection;

        protected override void Awake()
        {
            base.Awake();

            _playerManager = GetComponent<PlayerManager>();
        }

        public void HandleAllMovement()
        {
            HandleGroundedMovement();
            HandleRotation();
        }

        private void HandleGroundedMovement()
        {
            GetVerticalAndHorizontalInputs();

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

        private void GetVerticalAndHorizontalInputs()
        {
            _verticalMovement = PlayerInputManager.Instance.VerticalInput;
            _horizontalMovement = PlayerInputManager.Instance.HorizontalInput;

            //CLAMP MOVEMENTS
        }
    }
}

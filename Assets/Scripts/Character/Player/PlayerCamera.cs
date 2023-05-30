using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RVT
{
    public class PlayerCamera : MonoBehaviour
    {
        public static PlayerCamera Instance;

        [Header("Camera Settings")]
        [SerializeField] private float CameraSmoothSpeed = 1;
        [SerializeField] private float LeftRightRotSpeed = 220;
        [SerializeField] private float UpDownRotSpeed = 220;
        [SerializeField] private float MinPivot = -30;
        [SerializeField] private float MaxPivot = 60;
        [SerializeField] private float CameraCollisionRadius = 0.2f;
        [SerializeField] private LayerMask CollideWithLayers;

        [Header("Camera Values")]
        [SerializeField] private Transform CameraPivotTransform;
        [SerializeField] private float LeftRightLookAngle;
        [SerializeField] private float UpDownLookAngle;
        private Vector3 CameraVelocity;
        private Vector3 CameraObjectPosition;
        private float CameraZPosition;
        private float TargetCameraPosition;


        public Camera CameraObj;
        public PlayerManager Player;

        private void Awake()
        {
            if (Instance == null) { Instance = this; }
            else { Destroy(this); }
        }

        private void Start()
        {
            DontDestroyOnLoad(this);
            CameraZPosition = CameraObj.transform.localPosition.z;
        }

        public void HandleAllCameraActions()
        {
            if (Player != null)
            {
                HandleFollowTarget();
                HandleRotations();
                HandleCollissions();
            }
        }

        private void HandleFollowTarget()
        {
            Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, Player.transform.position, ref CameraVelocity, CameraSmoothSpeed * Time.deltaTime);
            transform.position = targetCameraPosition;
        }

        private void HandleRotations()
        {
            LeftRightLookAngle += (PlayerInputManager.Instance.CamHorizontalInput * LeftRightRotSpeed) * Time.deltaTime;
            UpDownLookAngle -= (PlayerInputManager.Instance.CamVerticalInput * UpDownRotSpeed) * Time.deltaTime;
            UpDownLookAngle = Mathf.Clamp(UpDownLookAngle, MinPivot, MaxPivot);

            Vector3 cameraRot = Vector3.zero;
            cameraRot.y = LeftRightLookAngle;
            Quaternion targetRot = Quaternion.Euler(cameraRot);
            transform.rotation = targetRot;

            cameraRot = Vector3.zero;
            cameraRot.x = UpDownLookAngle;
            targetRot = Quaternion.Euler(cameraRot);
            CameraPivotTransform.localRotation = targetRot;

        }

        private void HandleCollissions()
        {
            TargetCameraPosition = CameraZPosition;

            RaycastHit hit;
            Vector3 direction = CameraObj.transform.position - CameraPivotTransform.position;
            direction.Normalize();

            if (Physics.SphereCast(CameraPivotTransform.position, CameraCollisionRadius, direction, out hit, Mathf.Abs(TargetCameraPosition), CollideWithLayers))
            {
                float distanceFromHitObject = Vector3.Distance(CameraPivotTransform.position, hit.point);
                TargetCameraPosition = -(distanceFromHitObject - CameraCollisionRadius);
            }

            if (Mathf.Abs(TargetCameraPosition) < CameraCollisionRadius)
            {
                TargetCameraPosition = -CameraCollisionRadius;
            }

            CameraObjectPosition.z = Mathf.Lerp(CameraObj.transform.localPosition.z, TargetCameraPosition, 0.2f);
            CameraObj.transform.localPosition = CameraObjectPosition;
        }
    }
}

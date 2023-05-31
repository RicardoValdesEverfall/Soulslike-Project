using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RVT
{
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager Instance;
        public PlayerManager Player;

        [Header("CAMERA INPUT")]
        [SerializeField] public Vector2 CameraInput;
        [SerializeField] public float CamHorizontalInput;
        [SerializeField] public float CamVerticalInput;

        [Header("MOVEMENT INPUT")]
        [SerializeField] public Vector2 MovementInput;
        [SerializeField] public float HorizontalInput;
        [SerializeField] public float VerticalInput;
        [SerializeField] public float MoveAmount;

        [Header("ACTION INPUT")]
        [SerializeField] private bool DodgeInput;

        private PlayerControls PlayerControlsComponent;

        #region Script Voids
        private void Awake()
        {
            if (Instance == null) { Instance = this; }
            else { Destroy(this); }
        }

        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
            SceneManager.activeSceneChanged += OnSceneChange;
            Instance.enabled = false;
        }

        private void Update()
        {
            HandleCameraMovementInput();
            HandleMovementInput();
            HandleDodgeInput();
        }
        #endregion

        private void OnSceneChange(Scene oldScene, Scene newScene)
        {
            if (newScene.buildIndex == WorldSaveManager.Instance.GetWorldSceneIndex()) { Instance.enabled = true; }
            else { Instance.enabled = false; }
        }

        #region Script Utilities
        private void OnEnable()
        {
            if (PlayerControlsComponent == null)
            { 
                PlayerControlsComponent = new PlayerControls();
                PlayerControlsComponent.PlayerLocomotion.Movement.performed += i => MovementInput = i.ReadValue<Vector2>();
                PlayerControlsComponent.PlayerCamera.Movement.performed += i => CameraInput = i.ReadValue<Vector2>();
                PlayerControlsComponent.PlayerActions.Dodge.performed += i => DodgeInput = true;
            }

            PlayerControlsComponent.Enable();
        }

        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= OnSceneChange;
        }

        private void OnApplicationFocus(bool focus)
        {
            if (enabled)
            {
                if (focus) { PlayerControlsComponent.Enable(); }
                else { PlayerControlsComponent.Disable(); }
            }
        }
        #endregion

        #region Player Handles
        private void HandleMovementInput()
        {
            VerticalInput = MovementInput.y;
            HorizontalInput = MovementInput.x;

            MoveAmount = Mathf.Clamp01(Mathf.Abs(VerticalInput) + Mathf.Abs(HorizontalInput));

            if (MoveAmount <= 0.5 && MoveAmount > 0) { MoveAmount = 0.5f; }
            else if (MoveAmount > 0.5 && MoveAmount <= 1) { MoveAmount = 1f; }

            if (Player == null) { return; }

            Player._playerAnimator.UpdateAnimatorMovementParameters(0, MoveAmount);
        }

        private void HandleDodgeInput()
        {
            if (DodgeInput)
            {
                DodgeInput = false;

                Player._playerLocomotionManager.AttemptToPerformDodge();
            }
        }

        private void HandleCameraMovementInput()
        {
            CamHorizontalInput = CameraInput.x;
            CamVerticalInput = CameraInput.y;
        }
        #endregion
    }
}

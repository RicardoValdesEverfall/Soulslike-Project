using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RVT
{
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager Instance;

        [SerializeField] public Vector2 MovementInput;
        [SerializeField] public float HorizontalInput;
        [SerializeField] public float VerticalInput;
        [SerializeField] public float MoveAmount;

        private PlayerControls PlayerControlsComponent;

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
            HandleMovementInput();
        }

        private void OnSceneChange(Scene oldScene, Scene newScene)
        {
            if (newScene.buildIndex == WorldSaveManager.Instance.GetWorldSceneIndex()) { Instance.enabled = true; }
            else { Instance.enabled = false; }
        }

        private void OnEnable()
        {
            if (PlayerControlsComponent == null)
            { 
                PlayerControlsComponent = new PlayerControls();
                PlayerControlsComponent.PlayerMovement.Movement.performed += i => MovementInput = i.ReadValue<Vector2>();
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

        private void HandleMovementInput()
        {
            VerticalInput = MovementInput.y;
            HorizontalInput = MovementInput.x;

            MoveAmount = Mathf.Clamp01(Mathf.Abs(VerticalInput) + Mathf.Abs(HorizontalInput));

            if (MoveAmount <= 0.5 && MoveAmount > 0) { MoveAmount = 0.5f; }
            else if (MoveAmount > 0.5 && MoveAmount <= 1) { MoveAmount = 1f; }
        }
    }
}

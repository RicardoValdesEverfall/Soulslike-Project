using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RVT
{
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager Instance;

        [SerializeField] private Vector2 MovementInput;

        private PlayerControls PlayerControl;

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

        private void OnSceneChange(Scene oldScene, Scene newScene)
        {
            if (newScene.buildIndex == WorldSaveManager.Instance.GetWorldSceneIndex()) { Instance.enabled = true; }
            else { Instance.enabled = false; }
        }

        private void OnEnable()
        {
            if (PlayerControl == null)
            { 
                PlayerControl = new PlayerControls();
                PlayerControl.PlayerMovement.Movement.performed += i => MovementInput = i.ReadValue<Vector2>();
            }

            PlayerControl.Enable();
        }

        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= OnSceneChange;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace RVT
{
    public class PlayerUIManager : MonoBehaviour
    {
        public static PlayerUIManager Instance;

        [Header("NETWORK JOIN")]
        [SerializeField] private bool StartGameAsClient;

        [HideInInspector] public PlayerHUDManager PlayerHUDManager;

       private void Awake()
        {
            if (Instance == null) { Instance = this; }
            else { Destroy(this); }

            PlayerHUDManager = GetComponentInChildren<PlayerHUDManager>();
        }

        private void Start()
        {
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            if (StartGameAsClient)
            {
                StartGameAsClient = false;
                NetworkManager.Singleton.Shutdown();
                NetworkManager.Singleton.StartClient();
            }
        }
    }
}


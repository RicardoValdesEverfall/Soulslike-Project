using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace RVT
{
    public class PlayerUIManager : MonoBehaviour
    {
        [Header("NETWORK JOIN")]
        [SerializeField] private bool StartGameAsClient;

        public static PlayerUIManager Instance;

        private void Awake()
        {
            if (Instance == null) { Instance = this; }
            else { Destroy(this); }
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


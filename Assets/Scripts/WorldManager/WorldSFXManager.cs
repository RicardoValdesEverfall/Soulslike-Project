using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RVT
{
    public class WorldSFXManager : MonoBehaviour
    {
        public static WorldSFXManager Instance;

        [Header("Action SFX")]
        [SerializeField] public AudioClip rollSFX;

        private void Awake()
        {
            if (Instance == null) { Instance = this; }
            else { Destroy(this); }
        }

        private void Start()
        {
            DontDestroyOnLoad(this);
        }
    }
}

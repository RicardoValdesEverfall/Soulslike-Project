using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RVT
{
    public class PlayerCamera : MonoBehaviour
    {
        public static PlayerCamera Instance;
        public Camera CameraObj;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RVT
{
    public class WorldSaveManager : MonoBehaviour
    {
        public static WorldSaveManager Instance;

        [SerializeField] private int WorldSceneIndex;

        private void Awake()
        {
            if (Instance == null) { Instance = this; }
            else { Destroy(gameObject); }
        }

        private void Start()
        {
            DontDestroyOnLoad(this);
        }

        public IEnumerator LoadNewGame()
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(WorldSceneIndex);
            yield return null;
        }
    }
}

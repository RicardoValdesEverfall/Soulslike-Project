using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RVT
{
    public class CharacterManager : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}

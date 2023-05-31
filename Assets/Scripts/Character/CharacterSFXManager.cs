using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RVT
{
    public class CharacterSFXManager : MonoBehaviour
    {
        private AudioSource AudioSource;

        protected virtual void Awake()
        {
            AudioSource = GetComponent<AudioSource>();
        }

        public void PlayRollSFX()
        {
            AudioSource.PlayOneShot(WorldSFXManager.Instance.rollSFX);
        }
    }
}
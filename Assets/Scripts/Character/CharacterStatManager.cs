using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RVT
{
    public class CharacterStatManager : MonoBehaviour
    {
       
        public int CalculateStaminaBasedOnLevel(int endurance)
        {
            float stamina = 0;

            stamina = endurance * 10f;

            return Mathf.RoundToInt(stamina);
        }
    }
}

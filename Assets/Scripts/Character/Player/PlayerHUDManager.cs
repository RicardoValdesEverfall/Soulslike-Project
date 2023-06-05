using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RVT
{
    public class PlayerHUDManager : MonoBehaviour
    {
        [SerializeField] UIStatbar StaminaBar;

        public void SetNewStaminaValue(int oldval, int newval)
        {
            StaminaBar.SetStat(newval);
        }
    }
}

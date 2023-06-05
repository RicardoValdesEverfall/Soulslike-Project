using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RVT
{
    public class UIStatbar : MonoBehaviour
    {
        private Image SliderImg;



        protected virtual void Awake()
        {
            SliderImg = GetComponent<Image>();
        }

        public virtual void SetStat(int newval)
        {
            float fill = Mathf.Lerp(SliderImg.fillAmount, newval, 1);

            SliderImg.fillAmount = fill;
        }

        public virtual void SetStatMax()
        {

        }
    }
}

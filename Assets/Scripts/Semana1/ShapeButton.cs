using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Semana1
{
    public class ShapeButton : MonoBehaviour
    {
        public ShapeConfig shapeConfig;
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            GameConfigManager.Instance.SetShape(shapeConfig);
        }
    }
}

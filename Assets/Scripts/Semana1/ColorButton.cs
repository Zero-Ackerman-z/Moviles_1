using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.Scripts.Semana1
{
    public class ColorButton : MonoBehaviour
    {
        public ColorConfig colorConfig;
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnButtonClick);
        }
        private void OnButtonClick()
        {
            GameConfigManager.Instance.SetColor(colorConfig);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Semana1
{

    public class GameConfigManager : MonoBehaviour
    {
        public static GameConfigManager Instance;

        public Color selectedColor;
        public Sprite selectedShape;

        private Color defaultColor = Color.white; 

        private void Awake()
        {
            Instance = this;
        }

        public void SetColor(ColorConfig colorConfig)
        {
            selectedColor = colorConfig.color;
        }

        public void SetShape(ShapeConfig shapeConfig)
        {
            selectedShape = shapeConfig.shapeSprite;
            if (selectedColor == default) 
            {
                selectedColor = defaultColor;
            }
        }

        public bool CanInstantiate()
        {
            return selectedShape != null;
        }
    }

}

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Semana1
{
    [CreateAssetMenu(fileName = "NewShapeConfig", menuName = "Game/ShapeConfig")]
    public class ShapeConfig : ScriptableObject
    {
       
            public string shapeName;
            public Sprite shapeSprite;
 
    }
}
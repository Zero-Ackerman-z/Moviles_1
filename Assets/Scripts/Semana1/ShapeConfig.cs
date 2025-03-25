using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Semana1
{
    [CreateAssetMenu(fileName = "NewShapeConfig", menuName = "Game/ShapeConfig")]
    public class ShapeConfig : ScriptableObject
    {
       
            [SerializeField] private string shapeName;
            [SerializeField] public Sprite shapeSprite;
 
    }
}
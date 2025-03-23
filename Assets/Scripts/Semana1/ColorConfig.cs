using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Semana1
{
    [CreateAssetMenu(fileName = "NewColorConfig", menuName = "Game/ColorConfig")]
    public class ColorConfig : ScriptableObject
    {
       
            public string Name;
            public Color color;
    }
}
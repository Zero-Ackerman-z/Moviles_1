using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Semana1
{
    [CreateAssetMenu(fileName = "NewColorConfig", menuName = "Game/ColorConfig")]
    public class ColorConfig : ScriptableObject
    {
       
            [SerializeField] private string Name;
            [SerializeField] public Color color;
    }
}
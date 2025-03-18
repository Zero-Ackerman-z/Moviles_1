using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Semana1
{
    public class TouchTester : MonoBehaviour
    {
        public TextMeshProUGUI cube;
        public Vector2 position;
        // Use this for initialization
        void Start()
        {

        }

        void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    Debug.Log(touch.position);
                }
                if(touch.phase == TouchPhase.Moved)
                {
                    Debug.Log(touch.position);

                }

            }
        }
    }
}
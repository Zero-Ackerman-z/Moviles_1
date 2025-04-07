using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Semana1
{
    public class InputTapController : MonoBehaviour
    {
        [SerializeField] private TouchTester touchTester;

        private Camera mainCamera;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        public void OnTouch(InputAction.CallbackContext context)
        {
            if (context.started || context.performed || context.canceled)
            {
                var touch = Touchscreen.current.primaryTouch;

                if (touch.press.isPressed)
                {
                    Vector2 screenPosition = touch.position.ReadValue();

                    if (IsPointerOverUI(screenPosition)) return;

                    //touchTester.HandleTouch(touch.phase.ReadValue(), screenPosition);
                }
            }
        }

        private bool IsPointerOverUI(Vector2 position)
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
            {
                position = position
            };

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);

            return raycastResults.Count > 0;
        }
    }
}

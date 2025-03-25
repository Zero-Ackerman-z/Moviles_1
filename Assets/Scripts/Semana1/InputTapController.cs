using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace Assets.Scripts.Semana1
{
    public class InputTapController : MonoBehaviour
    {
        private TouchController touchController;
        public TouchTester touchTester;

        private void Awake()
        {
            touchController = new TouchController();
        }

        private void OnEnable()
        {
            touchController.Game.Enable();
            touchController.Game.Tap.performed += OnTap;
            touchController.Game.DoubleTap.performed += OnDoubleTap;
            touchController.Game.Press.canceled += OnPressCanceled;
            touchController.Game.Swipe.performed += OnSwipe;
        }

        private void OnDisable()
        {
            touchController.Game.Tap.performed -= OnTap;
            touchController.Game.DoubleTap.performed -= OnDoubleTap;
            touchController.Game.Press.canceled -= OnPressCanceled;
            touchController.Game.Swipe.performed -= OnSwipe;

            touchController.Game.Disable();
        }

        private void OnTap(InputAction.CallbackContext context)
        {
            Vector3 touchPos = GetTouchPosition();
            touchTester.CreateObject(touchPos);
        }

        private void OnDoubleTap(InputAction.CallbackContext context)
        {
            Vector3 touchPos = GetTouchPosition();
            touchTester.TryDeleteObject(touchPos);
        }

        private void OnPressCanceled(InputAction.CallbackContext context)
        {
            touchTester.DeleteAllObjects();
        }

        private void OnSwipe(InputAction.CallbackContext context)
        {
            touchTester.DeleteAllObjects();
        }

        private Vector3 GetTouchPosition()
        {
            Vector2 screenPos = touchController.Game.Tap.ReadValue<Vector2>();
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Camera.main.nearClipPlane));
            worldPos.z = 0f;
            return worldPos;
        }
    }
}

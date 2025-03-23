using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using UnityEngine.EventSystems;
namespace Assets.Scripts.Semana1
{
    public class TouchTester : MonoBehaviour
    {
        public GameObject prefab; 
        public GameObject trailPrefab; 

        private float lastTapTime = 0f;
        private float doubleTapThreshold = 0.3f; 
        private GameObject selectedObject = null; 
        private GameObject activeTrail = null; 

        private Vector2 startSwipePos; 
        private bool isSwiping = false; 
        private bool isDragging = false; 

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 touchPos = GetWorldPosition(touch.position);

                if (touch.phase == TouchPhase.Began)
                {
                    if (IsTouchOverUI(touch)) return;

                    startSwipePos = touch.position;
                    isSwiping = true;
                    isDragging = false; 

                    Collider2D hitCollider = Physics2D.OverlapPoint(touchPos);

                    // DOBLE TAP 
                    if (Time.time - lastTapTime < doubleTapThreshold && hitCollider != null)
                    {
                        TryDeleteObject(touchPos);
                        lastTapTime = 0;
                        return;
                    }

                    // TAP SIMPLE: 
                    if (hitCollider != null)
                    {
                        selectedObject = hitCollider.gameObject;
                        isDragging = true; 
                    }
                    else
                    {
                        CreateObject(touchPos); 
                    }

                    lastTapTime = Time.time;
                }

                // ARRASTRAR  OBJETO O TRAIL
                if (touch.phase == TouchPhase.Moved)
                {
                    if (selectedObject != null)
                    {
                        selectedObject.transform.position = touchPos;
                    }

                    
                    if (!isDragging)
                    {
                        if (activeTrail == null)
                        {
                            CreateTrail(touchPos);
                        }
                        else
                        {
                            activeTrail.transform.position = touchPos;
                        }
                    }
                }

                // DETECTAR SWIPE
                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    if (isSwiping && !isDragging)
                    {
                        float swipeDistance = Vector2.Distance(startSwipePos, touch.position);
                        if (swipeDistance > 200f) 
                        {
                            DeleteAllObjects();
                        }
                    }

                    selectedObject = null;
                    isSwiping = false;
                    isDragging = false;

                    
                    if (activeTrail != null)
                    {
                        Destroy(activeTrail, 0.5f);
                        activeTrail = null;
                    }
                }
            }
        }

        private bool IsTouchOverUI(Touch touch)
        {
            return EventSystem.current.IsPointerOverGameObject(touch.fingerId);
        }

        private Vector3 GetWorldPosition(Vector2 screenPosition)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, Camera.main.transform.position.z * -1));
            worldPosition.z = 0f;
            return worldPosition;
        }

        private void CreateObject(Vector3 position)
        {
            if (GameConfigManager.Instance.CanInstantiate())
            {
                GameObject newObject = Instantiate(prefab, position, Quaternion.identity);
                newObject.GetComponent<SpriteRenderer>().color = GameConfigManager.Instance.selectedColor;
                newObject.GetComponent<SpriteRenderer>().sprite = GameConfigManager.Instance.selectedShape;
                if (newObject.GetComponent<Collider2D>() == null)
                {
                    newObject.AddComponent<CircleCollider2D>();
                }
            }
        }

        private void TryDeleteObject(Vector3 position)
        {
            Collider2D hitCollider = Physics2D.OverlapPoint(position);
            if (hitCollider != null)
            {
                Destroy(hitCollider.gameObject);
            }
        }

        private void DeleteAllObjects()
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("InstancedObject"))
            {
                Destroy(obj);
            }
        }

        private void CreateTrail(Vector3 position)
        {
            if (trailPrefab != null && activeTrail == null) 
            {
                activeTrail = Instantiate(trailPrefab, position, Quaternion.identity);
                TrailRenderer trail = activeTrail.GetComponent<TrailRenderer>();
                if (trail != null)
                {
                    trail.startColor = GameConfigManager.Instance.selectedColor;
                    trail.endColor = new Color(GameConfigManager.Instance.selectedColor.r, GameConfigManager.Instance.selectedColor.g, GameConfigManager.Instance.selectedColor.b, 0f); // Desvanecer
                }
            }
        }
    }
}
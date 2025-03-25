using System.Collections;
using TMPro;
using UnityEngine;
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

        public void CreateObject(Vector2 screenPos)
        {
            Vector3 worldPos = GetWorldPosition(screenPos);

            if (GameConfigManager.Instance.CanInstantiate())
            {
                GameObject newObject = Instantiate(prefab, worldPos, Quaternion.identity);
                newObject.GetComponent<SpriteRenderer>().color = GameConfigManager.Instance.selectedColor;
                newObject.GetComponent<SpriteRenderer>().sprite = GameConfigManager.Instance.selectedShape;

                if (newObject.GetComponent<Collider2D>() == null)
                {
                    newObject.AddComponent<CircleCollider2D>();
                }
            }
        }

        public void TryDeleteObject(Vector2 screenPos)
        {
            Vector3 worldPos = GetWorldPosition(screenPos);
            Collider2D hitCollider = Physics2D.OverlapPoint(worldPos);

            if (hitCollider != null)
            {
                Destroy(hitCollider.gameObject);
            }
        }

        public void DeleteAllObjects()
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("InstancedObject"))
            {
                Destroy(obj);
            }
        }

        public void StartTrail(Vector2 screenPos)
        {
            Vector3 worldPos = GetWorldPosition(screenPos);

            if (trailPrefab != null && activeTrail == null)
            {
                activeTrail = Instantiate(trailPrefab, worldPos, Quaternion.identity);
                TrailRenderer trail = activeTrail.GetComponent<TrailRenderer>();
                if (trail != null)
                {
                    trail.startColor = GameConfigManager.Instance.selectedColor;
                    trail.endColor = new Color(GameConfigManager.Instance.selectedColor.r,
                                               GameConfigManager.Instance.selectedColor.g,
                                               GameConfigManager.Instance.selectedColor.b, 0f); // Desvanecer
                }
            }
        }

        public void UpdateTrail(Vector2 screenPos)
        {
            if (activeTrail != null)
            {
                activeTrail.transform.position = GetWorldPosition(screenPos);
            }
        }

        public void StopTrail()
        {
            if (activeTrail != null)
            {
                Destroy(activeTrail, 0.5f);
                activeTrail = null;
            }
        }

        private Vector3 GetWorldPosition(Vector2 screenPosition)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, Camera.main.nearClipPlane));
            worldPosition.z = 0f;
            return worldPosition;
        }
    }
}

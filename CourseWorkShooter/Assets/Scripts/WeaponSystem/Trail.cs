using System.Collections;
using UnityEngine;

namespace WeaponSystem
{
    public class Trail : MonoBehaviour
    {
        private const int TrailSpeed = 100;

        public IEnumerator ShowTrail(Vector3 startPoint, Vector3 endPoint)
        {
            float distance = Vector3.Distance(startPoint, endPoint);
            float remainingDistance = distance;
            
            while (remainingDistance > 0)
            {
                float step = Mathf.Clamp01(1 - remainingDistance / distance);
                transform.position = Vector3.Lerp(startPoint, endPoint, step);
                remainingDistance -= TrailSpeed * Time.deltaTime;
                yield return null;
            }
            
            transform.position = endPoint;
            Destroy(gameObject);
            yield return null;
        }
    }
}
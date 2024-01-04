using UnityEngine;

namespace WeaponSystem
{
    public class WeaponSway : MonoBehaviour
    {
        [SerializeField] private float _multiplier = 1.2f;
        [SerializeField] private float _maxAngle = 4;
        [SerializeField] private float _speed = 6;

        public void Perform(Vector2 lookInputVector)
        {
            Vector2 swayVector = new Vector2
            {
                x = Mathf.Clamp(lookInputVector.x * _multiplier, -_maxAngle, _maxAngle),
                y = Mathf.Clamp(lookInputVector.y * (-_multiplier), -_maxAngle, _maxAngle)
            };
            Quaternion swayRotation = Quaternion.Euler(new Vector3
            {
                x = swayVector.y,
                y = swayVector.x,
                z = swayVector.x
            });
            transform.localRotation = Quaternion.Lerp(transform.localRotation, swayRotation, _speed * Time.deltaTime);
        }
    }
}
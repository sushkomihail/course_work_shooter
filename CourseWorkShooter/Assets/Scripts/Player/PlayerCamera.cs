using UnityEngine;

namespace Player
{
    public class PlayerCamera : MonoBehaviour
    {
        public float sensX;
        public float sensY; 

        public GameObject obj;

        public Transform orientation;

        float xRotation;
        float yRotation;

        private void Update() {
            float mouseX = Input.GetAxisRaw("Mouse X");
            float mouseY = Input.GetAxisRaw("Mouse Y");

            yRotation +=mouseX;
            xRotation -=mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

    
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0,yRotation,0);
        }
    }
}
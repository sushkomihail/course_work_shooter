using UnityEngine;

namespace SceneLoadingSystem
{
    public class LoadingCallbackController : MonoBehaviour
    {
        private bool _isFirstUpdate = true;

        private void Update()
        {
            if (_isFirstUpdate)
            {
                SceneLoader.TryProcessCallback();
                _isFirstUpdate = false;
            }
        }
    }
}
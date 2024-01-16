using System;
using SceneLoadingSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SceneTransition : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Scenes _targetScene;

        private void Awake()
        {
            _button.onClick.AddListener(() => SceneLoader.LoadCallback(_targetScene));
        }
    }
}

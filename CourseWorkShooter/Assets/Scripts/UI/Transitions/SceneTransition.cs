using SceneLoadingSystem;
using UnityEngine;

namespace UI.Transitions
{
    public class SceneTransition : Transition
    {
        [SerializeField] private Scenes _targetScene;

        protected override void Perform()
        {
            SceneLoader.LoadCallback(_targetScene);
        }
    }
}

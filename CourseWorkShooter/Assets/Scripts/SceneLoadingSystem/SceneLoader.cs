using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace SceneLoadingSystem
{
    public static class SceneLoader
    {
        private static readonly Dictionary<Scenes, int> _scenesDictionary = new Dictionary<Scenes, int>
        {
            {Scenes.Menu, 0},
            {Scenes.Loading, 1},
            {Scenes.Game, 2}
        };

        private static Action _onLoadCallback;

        public static void LoadCallback(Scenes nextScene)
        {
            _onLoadCallback = () => SceneManager.LoadScene(_scenesDictionary[nextScene]);
            SceneManager.LoadScene(_scenesDictionary[Scenes.Loading]);
        }

        public static void TryProcessCallback()
        {
            _onLoadCallback?.Invoke();
            _onLoadCallback = null;
        }
    }
}
using System.Collections.Generic;

namespace PauseSystem
{
    public class PauseManager
    {
        private readonly List<IPauseHandler> _handlers;

        public bool IsPaused { get; private set; }

        public PauseManager()
        {
            _handlers = new List<IPauseHandler>();
        }

        public void AddHandler(IPauseHandler handler)
        {
            _handlers.Add(handler);
        }
        
        public void OnPause()
        {
            IsPaused = !IsPaused;

            if (_handlers.Count == 0) return;
            
            foreach (IPauseHandler handler in _handlers)
            {
                handler.OnPause(IsPaused);
            }
        }
    }
}
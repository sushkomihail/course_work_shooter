using System;

namespace SaveSystem
{
    [Serializable]
    public class StatsData
    {
        public int Score { get; }
        public float Time { get; }
        
        public StatsData(int score, float time)
        {
            Score = score;
            Time = time;
        }
    }
}
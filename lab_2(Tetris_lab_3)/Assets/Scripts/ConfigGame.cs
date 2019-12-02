using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Config", menuName = "Config", order = 1)]
    public class ConfigGame : ScriptableObject
    {
        public float fallSpeed = 1.0f;
        public GameObject[] groups;
        public int scoreOneLine = 5;
        public int scoreTwoLine = 15;
        public int scoreThreeLine = 45;
        public int scoreFourLine = 150;

        public float FallSpeed
        {
            get => fallSpeed;
            set => fallSpeed = value;
        }

        public GameObject[] Groups => groups;
        public int ScoreOneLine => scoreOneLine;
        public int ScoreTwoLine => scoreTwoLine;
        public int ScoreThreeLine => scoreThreeLine;
        public int ScoreFourLine => scoreFourLine;
    }
}
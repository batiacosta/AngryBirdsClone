using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class ScoreManagerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreTMP;
        private int _score;

        private void Start()
        {
            _score = 0;
            scoreTMP.text = $"Score: {_score}";
        }

        public void UpdateScore(int scoreValue)
        {
            _score = scoreValue;
            scoreTMP.text = $"Score: {_score}";
        }
    }
}
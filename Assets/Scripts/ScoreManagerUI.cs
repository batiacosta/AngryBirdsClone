using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class ScoreManagerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreTMP;
        private int _score;
        private Animator _animator;
        private static readonly int IsHit = Animator.StringToHash("IsHit");

        private void Start()
        {
            _score = 0;
            scoreTMP.text = $"Score: {_score}";
            _animator = GetComponent<Animator>();
        }

        public void UpdateScore(int scoreValue)
        {
            _score = scoreValue;
            scoreTMP.text = $"Score: {_score}";
            _animator.SetBool(IsHit, true);
            StartCoroutine(BackToIdle());
        }

        private IEnumerator BackToIdle()
        {
            yield return new WaitForSeconds(0.2f);
            _animator.SetBool(IsHit, false);
        }
    }
}
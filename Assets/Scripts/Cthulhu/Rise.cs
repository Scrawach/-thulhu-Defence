using System;
using System.Collections;
using Player;
using UnityEngine;

namespace Cthulhu
{
    public class Rise : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _effect;

        [SerializeField] 
        private float _time;

        [SerializeField] 
        private GameObject _cthulhu;
        
        [SerializeField] 
        private GameObject _destroySphere;

        [SerializeField] 
        private GameObject _audio;

        private Score _score;
        private bool _gameOver;
        
        public event Action Win;

        public void Construct(Score score)
        {
            _score = score;
            _score.Changed += OnScoreChanged;
        }

        public void Lock()
        {
            _gameOver = true;
        }

        private void OnScoreChanged(int obj)
        {
            if (_gameOver)
                return;
            
            if (obj >= _score.TargetValue)
            {
                StartAnim();
            }
        }

        public void StartAnim()
        {
            GetComponent<Animator>().SetTrigger("GameEnd");
            _destroySphere.SetActive(true);
        }

        public void Spawn()
        {
            Instantiate(_effect, transform.position, Quaternion.identity);
            StartCoroutine(Waiting());
        }

        private IEnumerator Waiting()
        {

            yield return new WaitForSeconds(_time);
            _cthulhu.SetActive(true);
            _audio.SetActive(true);
            
            yield return new WaitForSeconds(4f);
            Win?.Invoke();
        }
    }
}
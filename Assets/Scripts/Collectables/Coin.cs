using System;
using UnityEngine;
using UnityEngine.Events;

namespace Collectables
{
    public class Coin: MonoBehaviour, ICollectable<int>
    {
        [SerializeField] 
        private int _value;
        
        public int Value => _value;

        [SerializeField] 
        private UnityEvent<int> OnCollected;

        public void Collect()
        {
            GameManager.Instance.Score.AddPoints(Value);
            GameManager.Instance.GetMainUI.UpdateScore(GameManager.Instance.Score);
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(!col.CompareTag("Player")) return;
            OnCollected.Invoke(_value);
        }
    }
}
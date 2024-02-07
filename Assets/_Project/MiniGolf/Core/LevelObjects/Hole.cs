using System;
using UnityEngine;

namespace MiniGolf.Core.LevelObjects
{
    [RequireComponent(typeof(Collider))]
    internal class Hole : MonoBehaviour
    {
        public event Action OnLevelFinish;
        
        [SerializeField] private Collider col;

        private void Reset()
        {
            col = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;
            
            OnLevelFinish?.Invoke();
        }
    }
}
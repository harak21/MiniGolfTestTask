using System;
using UnityEngine;

namespace MiniGolf.Core.LevelObjects
{
    [RequireComponent(typeof(Collider))]
    internal class Booster : MonoBehaviour
    {
        public event Action<Vector3> OnBallBust; 
        
        [SerializeField] private Collider col;
        private void Reset()
        {
            col = GetComponent<Collider>();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;
            
            OnBallBust?.Invoke(transform.forward);
        }
    }
}
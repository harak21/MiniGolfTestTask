using System;
using UnityEngine;

namespace MiniGolf.Core.LevelObjects
{
    [RequireComponent(typeof(Collider))]
    internal class Hole : MonoBehaviour
    {
        public event Action OnLevelFinish;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;
            
            OnLevelFinish?.Invoke();
        }
    }
}
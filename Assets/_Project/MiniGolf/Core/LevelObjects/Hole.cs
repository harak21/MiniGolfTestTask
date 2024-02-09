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

            other.gameObject.layer = LayerMask.NameToLayer("PlayerInHole");
            OnLevelFinish?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;
            
            other.gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
}
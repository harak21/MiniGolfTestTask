using System;
using System.Diagnostics;
using UnityEngine;

namespace MiniGolf.Core.LevelObjects
{
    [RequireComponent(typeof(Collider))]
    internal class Booster : MonoBehaviour
    {
        public event Action<Vector3> OnBallBust;

        [SerializeField] private Vector3 boostDirection;
        [SerializeField] private float boostMultiplier;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;
            
            OnBallBust?.Invoke(transform.TransformDirection(boostDirection) * boostMultiplier);
        }

        [Conditional("UNITY_EDITOR")]
        private void OnDrawGizmos()
        {
            var position = transform.position;
            var dir = transform.TransformDirection(boostDirection);
            Gizmos.color = Color.black;
            Gizmos.DrawLine(position, position + dir);
        }
    }
}
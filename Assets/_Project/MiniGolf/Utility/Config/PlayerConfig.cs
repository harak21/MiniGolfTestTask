using UnityEngine;

namespace MiniGolf.Utility.Config
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "MiniGolf/PlayerConfig", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private float inputForceMultiplier = 1.6f;
        [SerializeField] private float maxForce = 15f;
        [SerializeField] private Gradient arrowGradient;

        public float InputForceMultiplier => inputForceMultiplier;

        public float MaxForce => maxForce;

        public Gradient ArrowGradient => arrowGradient;
    }
}
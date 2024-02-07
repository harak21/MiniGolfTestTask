using System.Collections.Generic;
using UnityEngine;

namespace MiniGolf.Core.LevelObjects
{
    internal class LevelData : MonoBehaviour
    {
        [SerializeField] private Hole hole;
        [SerializeField] private List<Booster> boosters;
    }
}
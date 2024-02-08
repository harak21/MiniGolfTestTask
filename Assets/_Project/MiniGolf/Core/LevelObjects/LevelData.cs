using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace MiniGolf.Core.LevelObjects
{
    internal class LevelData : MonoBehaviour
    {
        [SerializeField] private Hole hole;
        [SerializeField] private List<Booster> boosters;

        public Hole Hole => hole;
        public ReadOnlyCollection<Booster> Boosters => boosters.AsReadOnly();
    }
}
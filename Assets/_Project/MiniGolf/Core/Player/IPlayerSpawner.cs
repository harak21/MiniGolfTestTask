using MiniGolf.Utility.Loading;
using UnityEngine;

namespace MiniGolf.Core.Player
{
    internal interface IPlayerSpawner : ILoadUnit
    {
        GameObject Spawn();
    }
}
using MiniGolf.Utility.Loading;
using UnityEngine;

namespace MiniGolf.Core.Player
{
    internal interface IPlayerSpawner : ILoadUnit
    {
        GameObject Spawn();

        /// <summary>
        /// you can make a pool here if you need to.
        /// </summary>
        /// <param name="gameObject"></param>
        void Release(GameObject gameObject);
    }
}
using UnityEngine;
using UnityEngine.AI;

namespace Game.Scripts
{
    public static class Utils
    {
        private const float RangeXMin = -10.0f;
        private const float RangeXMax = 10.0f;
        private const float RangeZMin = -12.0f;
        private const float RangeZMax = 7.0f;

        public static Vector3 GetRandomPoint()
        {
            for (int i = 0; i < 100; i++)
            {
                Vector3 randomPoint = new Vector3(
                    Random.Range(RangeXMin, RangeXMax),
                    0,
                    Random.Range(RangeZMin, RangeZMax)
                );
                if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 0.1f, NavMesh.AllAreas))
                {
                    return hit.position;
                }
            }
            return Vector3.zero;
        }
    }
}
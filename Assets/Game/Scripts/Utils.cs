using System.Collections.Generic;
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
        private const float MinimumDistanceBetweenTreasure = 5.0f;
        private static readonly HashSet<Vector3> PreviousTreasurePositions = new HashSet<Vector3>();

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

        public static Vector3 GetRandomPoint(Vector3 center, float minDist, float maxDist)
        {
            NavMeshHit hit;
            for (int i = 0; i < 300; i++)
            {
                Vector3 randInSphere = Random.insideUnitSphere;
                Vector3 randPointToRight = center + new Vector3(Mathf.Abs(randInSphere.x), randInSphere.y, randInSphere.z) * Random.Range(minDist, maxDist);
                if (NavMesh.SamplePosition(randPointToRight, out hit, 20f, NavMesh.AllAreas))
                {
                    return hit.position;
                }
            }
            Debug.Log("nothing");
            return Vector3.zero;
        }

        public static Vector3 GetTreasureRandomPoint()
        {
            for (int i = 0; i < 100; i++)
            {
                Vector3 randomPoint = new Vector3(
                    Random.Range(RangeXMin, RangeXMax),
                    0,
                    Random.Range(RangeZMin, RangeZMax)
                );
                if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 0.1f, NavMesh.AllAreas) &&
                    !TooCloseWithOtherTreasures(hit.position))
                {
                    return hit.position;
                }
            }
            return Vector3.zero;
        }

        private static bool TooCloseWithOtherTreasures(Vector3 newTreasurePosition)
        {
            foreach (Vector3 otherTreasureLocation in PreviousTreasurePositions)
            {
                if (Vector3.Distance(newTreasurePosition, otherTreasureLocation) <= MinimumDistanceBetweenTreasure)
                {
                    return true;
                }
            }
            PreviousTreasurePositions.Add(newTreasurePosition);
            return false;
        }
    }
}
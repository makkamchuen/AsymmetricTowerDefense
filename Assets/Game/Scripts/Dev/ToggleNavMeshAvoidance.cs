using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ATD.Dev
{
    public class ToggleNavMeshAvoidance : MonoBehaviour
    {
        [SerializeField] bool Toggle = false;
        NavMeshAgent[] childNMA;

        void Start()
        {
            childNMA = GetComponentsInChildren<NavMeshAgent>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.O)) { ToggleNMA(); }
        }

        private void ToggleNMA()
        {
            if (Toggle == false)
            {
                foreach (NavMeshAgent NMA in childNMA)
                {
                    NMA.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
                }
                Toggle = true;
            }
            else
            {
                foreach (NavMeshAgent NMA in childNMA)
                {
                    NMA.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
                }
                Toggle = false;
            }
        }
    }
}
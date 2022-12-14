using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Utilities
{
    public class Utils : MonoBehaviour
    {
        /// <summary>
        /// Generates positions on a nav mesh within a radius around the origin
        /// </summary>
        /// <param name="origin">The center of sphere</param>
        /// <param name="searchPoints">Amount of positions to generate</param>
        /// <param name="searchRadius">How large an area to generate points</param>
        /// <returns>Vector3 list of points on the nav mesh</returns>
        public static List<Vector3> GenerateSearchPoints(Vector3 origin, int searchPoints, int searchRadius) // Generates a list of positions based on an area for the guard to travel to
        {
            List<Vector3> searchPositions = new List<Vector3>();
            for (int i = 0; i < searchPoints; i++)
            {
                Vector3 newPos = RandomNavSphere(origin, searchRadius, -1);
                if(newPos != Vector3.zero) searchPositions.Add(newPos);
            }

            return searchPositions;
        }

        private static Vector3 RandomNavSphere (Vector3 origin, float distance, int layermask) 
        {
            /*************************************************************************************************
            *    Title: Random "Wander" AI using NavMesh
            *    Author: MysterySoftware
            *    Date: 2015
            *    Code version: 1.0
            *    Availability: https://forum.unity.com/threads/solved-random-wander-ai-using-navmesh.327950/
            **************************************************************************************************/

            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        
            randomDirection += origin;

            if (NavMesh.SamplePosition(randomDirection, out var navHit, distance, layermask))
            {
                return navHit.position;
            }

            return Vector3.zero;

        }
        
        /*************************************************************************************************
            *    Title: NavMeshPath.corners
            *    Author: Unity Technologies
            *    Date: 2022
            *    Code version: 1.0
            *    Availability: https://docs.unity3d.com/ScriptReference/AI.NavMeshPath-corners.html
            **************************************************************************************************/
        public static float CalculatePathLength(NavMeshPath path) {
            if (path.corners.Length < 2)
                return 0;
        
            float lengthSoFar = 0.0F;
            for (int i = 1; i < path.corners.Length; i++) {
                lengthSoFar += Vector3.Distance(path.corners[i - 1], path.corners[i]);
            }
            return lengthSoFar;
        }
    }
}
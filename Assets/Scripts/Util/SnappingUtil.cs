using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Util
{
    public static class SnappingUtil
    {
        /// <summary>
        /// Tries to find nearest snap point in a specific radius for an object
        /// </summary>
        /// <param name="nearPoint">Point from where to look for snap-points</param>
        /// <param name="nearDistance">Radius</param>
        /// <param name="offset">Distance between object and snap point</param>
        /// <param name="entity">Object to snap</param>
        /// <param name="snapPoint">The point that was found</param>
        /// <returns>Whether the search was successful</returns>
        public static bool TryGetSnappingPoint(Vector3 nearPoint, float nearDistance, float offset, GameObject entity, out Vector3 snapPoint)
        {
            IEnumerable<GameObject> walkways = GameObject.FindGameObjectsWithTag("Walkway")
                                                         .Where(walkway => walkway != entity);

            if (!IsNearSnappingPoint(nearPoint, nearDistance, walkways, out snapPoint)) { return false; }

            // Transforms snap-point to valid building point
            BoxCollider entityCollider = entity.GetComponent<BoxCollider>();

            Vector3 closest = entityCollider.ClosestPointOnBounds(snapPoint);
            Vector3 direction = (closest - snapPoint).normalized;
            Vector3 entityCentre = entityCollider.bounds.extents;

            Vector3 localDistance = new Vector3(
                direction.x * (entityCentre.x + offset),
                0,
                direction.z * (entityCentre.z + offset)
            );

            snapPoint += localDistance;

            return true;
        }

        /// <summary>
        /// Checks if the given point is near any snapping point in a list of snappable game-objects.
        /// </summary>
        /// <param name="point">Starting point</param>
        /// <param name="nearDistance">Distance in which to look</param>
        /// <param name="snappables">Possible objects to snap to</param>
        /// <param name="snapPoint">The point that was found</param>
        /// <returns>Whether the search was successful</returns>
        public static bool IsNearSnappingPoint(Vector3 point, float nearDistance, IEnumerable<GameObject> snappables, out Vector3 snapPoint)
        {
            Vector3? possibleSnapPoint = GetNearestSnapPoint(point, snappables);

            if (possibleSnapPoint == null)
            {
                snapPoint = Vector3.zero;

                return false;
            }

            // Nearest point on the gameobject to snap to
            snapPoint = (Vector3) possibleSnapPoint;
            snapPoint.y = 0;

            return Vector3.Distance(snapPoint, point) < nearDistance;
        }

        /// <summary>
        /// Finds the nearest snapping point
        /// </summary>
        /// <param name="toPoint">Starting point</param>
        /// <param name="snappables">Possible objects to snap to</param>
        /// <returns>Nearest point or null if no points were found</returns>
        public static Vector3? GetNearestSnapPoint(Vector3 toPoint, IEnumerable<GameObject> snappables)
        {
            IEnumerable<GameObject> gameObjects = snappables as GameObject[] ?? snappables.ToArray();

            if (!gameObjects.Any()) { return null; }

            IEnumerable<Vector3> snapPoints = gameObjects.Select(walkway => walkway.GetComponent<BoxCollider>().ClosestPoint(toPoint))
                                                         .OrderBy(snapPoint => Vector3.Distance(snapPoint, toPoint));

            return snapPoints.FirstOrDefault();
        }
    }
}
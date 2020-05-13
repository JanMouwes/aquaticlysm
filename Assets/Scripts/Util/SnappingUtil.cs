using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Util
{
    public static class SnappingUtil
    {
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

        public static Vector3? GetNearestSnapPoint(Vector3 toPoint, IEnumerable<GameObject> walkways)
        {
            IEnumerable<Vector3> snapPoints = walkways.Select(walkway => walkway.GetComponent<BoxCollider>().ClosestPoint(toPoint))
                                                      .OrderBy(snapPoint => Vector3.Distance(snapPoint, toPoint))
                                                      .ToList();

            return snapPoints.Any() ? (Vector3?) snapPoints.FirstOrDefault() : null;
        }
    }
}
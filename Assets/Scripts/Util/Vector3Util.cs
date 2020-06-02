using UnityEngine;

namespace Util
{
    public static class Vector3Util
    {
        public static Vector3 RandomVector3InRange(Vector3 centre, float xRange, float yRange, float zRange)
        {
            return centre + RandomVector3(xRange, yRange, zRange);
        }

        public static Vector3 RandomVector3(float xRange, float yRange, float zRange)
        {
            return new Vector3
            {
                x = Random.Range(-xRange, xRange),
                y = Random.Range(-yRange, yRange),
                z = Random.Range(-zRange, zRange),
            };
        }

        public static Vector3 RandomVector3(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
        {
            return new Vector3
            {
                x = Random.Range(minX, maxX),
                y = Random.Range(minY, maxY),
                z = Random.Range(minZ, maxZ),
            };
        }
    }
}
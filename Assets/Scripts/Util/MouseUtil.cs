using UnityEngine;

namespace Util
{
    public static class MouseUtil
    {
        /// <summary>
        /// Raycasts at the mouse's screen position.
        /// </summary>
        /// <param name="hit">RaycastHit if successful</param>
        /// <returns>True if successful, false if Camera.main is null OR ray cast was not successful</returns>
        public static bool TryRaycastAtMousePosition(out RaycastHit hit) => TryRaycastAtMousePosition(Mathf.Infinity, out hit);

        /// <summary>
        /// Raycasts at the mouse's screen position.
        /// </summary>
        /// <param name="maxDistance">maximum distance to try for a raycast-hit</param>
        /// <param name="hit">RaycastHit if successful</param>
        /// <returns>True if successful, false if Camera.main is null OR ray cast was not successful</returns>
        public static bool TryRaycastAtMousePosition(float maxDistance, out RaycastHit hit)
        {
            Camera camera = Camera.main;
            
            if (camera == null)
            {
                hit = default;

                return false;
            }

            return Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, maxDistance);
        }
    }
}
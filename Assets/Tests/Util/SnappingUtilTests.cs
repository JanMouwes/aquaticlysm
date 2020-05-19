using System.Collections;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Util;

namespace Tests.Util
{
    public class SnappingUtilTests
    {
        private static GameObject CreateTestGameObjectWithBoxCollider(Vector2 position = default)
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent<BoxCollider>();
            gameObject.transform.position = position;

            BoxCollider subjectBoxCollider = gameObject.GetComponent<BoxCollider>();
            subjectBoxCollider.size = Vector3.zero;
            subjectBoxCollider.center = new Vector3(2, 0, 2);

            return gameObject;
        }


        [Test]
        public void Test_WhenNearestSnapPointCalled_ShouldReturnNearestSnapPoint()
        {
            GameObject gameObject = CreateTestGameObjectWithBoxCollider();

            IEnumerable<GameObject> gameObjects = new[] {gameObject};

            Vector3? actual = SnappingUtil.GetNearestSnapPoint(new Vector3(1, 0, 1), gameObjects);

            Assert.IsNotNull(actual);

            Vector3 actualNotNull = (Vector3) actual;

            // Use the Assert class to test conditions
            Assert.AreEqual(2f, actualNotNull.x, .001f);
            Assert.AreEqual(0f, actualNotNull.y, .001f);
            Assert.AreEqual(2f, actualNotNull.z, .001f);
        }

        [TestCase(1, 1, 1f, false)]
        [TestCase(1, 1, 3f, true)]
        public void Test_WhenIsNearSnappingPoint_ShouldReturnTrue(float x, float z, float distance, bool expected)
        {
            GameObject gameObject = CreateTestGameObjectWithBoxCollider();

            IEnumerable<GameObject> gameObjects = new[] {gameObject};

            bool actual = SnappingUtil.IsNearSnappingPoint(new Vector3(x, 0, z), distance, gameObjects, out Vector3 actualSnapPoint);

            // Use the Assert class to test conditions
            Assert.AreEqual(expected, actual);
        }

        [TestCase(1, 1, 1f, 1f, false)]
        [TestCase(1, 1, 3f, 1f, true)]
        public void Test_WhenTryGetSnappingPointCalled_ShouldReturnExpected(float x, float z, float distance, float offset, bool expected)
        {
            GameObject subject = CreateTestGameObjectWithBoxCollider();
            GameObject gameObject = CreateTestGameObjectWithBoxCollider();

            IEnumerable<GameObject> gameObjects = new[] {gameObject};

            bool actual = SnappingUtil.TryGetSnappingPoint(new Vector3(x, 0, z), distance, offset, subject, gameObjects, out Vector3 actualSnapPoint);

            // Use the Assert class to test conditions
            Assert.AreEqual(expected, actual);
        }
    }
}
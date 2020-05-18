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
        // A Test behaves as an ordinary method
        [Test]
        public void Test_WhenNearestSnapPointCalled_ShouldReturnNearestSnapPoint()
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent<BoxCollider>();

            BoxCollider boxColliderMock = gameObject.GetComponent<BoxCollider>();
            boxColliderMock.size = Vector3.zero;
            boxColliderMock.center = new Vector3(2, 0, 2);

            IEnumerable<GameObject> gameObjects = new[] {gameObject};

            Vector3? result = SnappingUtil.GetNearestSnapPoint(new Vector3(1, 0, 1), gameObjects);

            Assert.IsNotNull(result);

            Vector3 actualNotNull = (Vector3) result;

            // Use the Assert class to test conditions
            Assert.AreEqual(2f, actualNotNull.x, .001f);

            Assert.AreEqual(0f, actualNotNull.y, .001f);

            Assert.AreEqual(2f, actualNotNull.z, .001f);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator Test_WithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
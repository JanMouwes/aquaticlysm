using System.Collections;
using System.Collections.Generic;
//using Moq;
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
        public void Test_SimplePasses()
        {
            //Mock<BoxCollider> boxColliderMock = new Mock<BoxCollider>(MockBehavior.Strict);
            //Mock<GameObject> gameObjectMock = new Mock<GameObject>(MockBehavior.Strict);
            //IEnumerable<GameObject> gameObjects = new GameObject[] { gameObjectMock.Object }; 

            //boxColliderMock.Setup(p => p.ClosestPoint(It.IsAny<Vector3>()))
            //               .Returns(new Vector3(2, 0, 2));

            //gameObjectMock.Setup(p => p.GetComponent<BoxCollider>())
            //   .Returns(boxColliderMock.Object);

            //Vector3? result = SnappingUtil.GetNearestSnapPoint(new Vector3(1, 0, 1), gameObjects);

            //Assert.IsNotNull(result);

            //Vector3 actualNotNull = (Vector3)result;

            //// Use the Assert class to test conditions
            //Assert.AreEqual(actualNotNull.x, 2f);

            //Assert.AreEqual(actualNotNull.y, 0f);

            //Assert.AreEqual(actualNotNull.z, 2f);
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

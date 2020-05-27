using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class DissolveTest
    {
        [UnityTest]
        public IEnumerator WhenAFractionBuild_ShouldBeNotBuild()
        {
            //Arrange
            GameObject gameObject = new GameObject();
            DissolveController dissolveController = gameObject.AddComponent<DissolveController>();

            yield return null;

            //Act & Assert
            Assert.IsFalse(dissolveController.Build(0.1f));
        }

        [UnityTest]
        public IEnumerator WhenMultipleFractionsBuild_ShouldBeBuild()
        {
            //Arrange
            GameObject gameObject = new GameObject();
            DissolveController dissolveController = gameObject.AddComponent<DissolveController>();

            yield return null;

            //Act
            dissolveController.Build(0.1f);
            dissolveController.Build(0.1f);
            dissolveController.Build(0.1f);

            //Assert
            Assert.IsFalse(dissolveController.Build(0.1f));
        }

        [UnityTest]
        public IEnumerator WhenCompletelyBuild_ShouldBeBuild()
        {
            //Arrange
            GameObject gameObject = new GameObject();
            DissolveController dissolveController = gameObject.AddComponent<DissolveController>();

            yield return null;

            //Act & Assert
            Assert.IsTrue(dissolveController.Build(1f));
        }

        [UnityTest]
        public IEnumerator WhenMultipleFractionsBuildWithOneTotal_ShouldBeBuild()
        {
            //Arrange
            GameObject gameObject = new GameObject();
            DissolveController dissolveController = gameObject.AddComponent<DissolveController>();

            yield return null;

            //Act
            dissolveController.Build(0.1f);
            dissolveController.Build(0.3f);
            dissolveController.Build(0.5f);

            //Assert
            Assert.IsTrue(dissolveController.Build(0.1f));
        }

        [UnityTest]
        public IEnumerator WhenBuildTooMuch_ShouldBeBuild()
        {
            //Arrange
            GameObject gameObject = new GameObject();
            DissolveController dissolveController = gameObject.AddComponent<DissolveController>();

            yield return null;

            //Act
            dissolveController.Build(0.4f);
            dissolveController.Build(0.3f);
            dissolveController.Build(0.5f);

            //Assert
            Assert.IsTrue(dissolveController.Build(0.1f));
        }
    }
}

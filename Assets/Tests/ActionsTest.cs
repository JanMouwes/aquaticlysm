using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ActionsTest
    {
        [Test]
        public void WhenDefinedAction_ShouldDoAction()
        {
            // Arrange
            Character character = new Character();

            // Act
            bool actionExcists = character.ActionHandler("Rest", Vector3.zero, false);

            // Assert
            Assert.IsTrue(actionExcists);
        }        

        [UnityTest]
        public IEnumerator WhenUndefinedAction_ShouldNotDoAnything()
        {
            // Arrange
            GameObject gameObject = new GameObject("dang");
            Character character = gameObject.AddComponent<Character>();

            // Act
            bool actionExcists = character.ActionHandler("Oewagadoego", Vector3.zero, false);

            yield return null;

            // Assert
            Assert.IsFalse(actionExcists);
        }
    }
}

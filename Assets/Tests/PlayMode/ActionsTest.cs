using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class ActionsTest
    {
            

        [UnityTest]
        public IEnumerator WhenUndefinedAction_ShouldNotDoAnything()
        {
            // Arrange
            GameObject gameObject = new GameObject("dang");
            SceneManager.LoadScene("Scenes/LocalSea");
            Object.Instantiate(gameObject);
            Character character = gameObject.AddComponent<Character>();
            yield return new WaitForSeconds(0.5f);
            // Act
            bool actionExcists = character.ActionHandler("Oewagadoego", Vector3.zero, false);


            // Assert
            Assert.IsFalse(actionExcists);
        }
        [UnityTest]
        public IEnumerator WhenActionExist_ShouldExist()
        {
            // Arrange
            GameObject gameObject = new GameObject("dang");
            SceneManager.LoadScene("Scenes/LocalSea");
            Object.Instantiate(gameObject);
            Character character = gameObject.AddComponent<Character>();
            yield return new WaitForSeconds(0.5f);
            // Act
            bool actionExcists = character.ActionHandler("Rest", Vector3.zero, false);


            // Assert
            Assert.IsTrue(actionExcists);
        }
    }
}

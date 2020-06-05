using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class GlobalStateMachineTest
    {
        [UnityTest]
        public IEnumerator WhenLoaded_ShouldBePlayState()
        {
            //Arrange
            GameObject gameObject = new GameObject();
            gameObject.AddComponent<GlobalStateMachine>();

            IState currentState = null;

            GlobalStateMachine.instance.StateChanged += (IState s) => currentState = s;

            //Act
            yield return null;

            //Assert
            Assert.IsTrue(currentState is PlayState);
        }

        [UnityTest]
        public IEnumerator WhenStateChanged_ShouldChangeState()
        {
            //Arrange
            GameObject gameObject = new GameObject();
            gameObject.AddComponent<GlobalStateMachine>();

            IState currentState = null;
            IState state = new BuildState();

            GlobalStateMachine.instance.StateChanged += (IState s) => currentState = s;

            //Act
            yield return null;
            GlobalStateMachine.instance.ChangeState(state);

            //Assert
            Assert.AreEqual(state, currentState);
        }
    }
}

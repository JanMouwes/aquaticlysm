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
        public IEnumerator WhenStateChanged_ShouldChangeState()
        {
            //Arrange
            SceneManager.LoadScene("Scenes/LocalSea");
            IState currentState = null;
            IState state = new BuildState();

            //Act
            GlobalStateMachine.instance.StateChanged += (IState s) => currentState = s;
            GlobalStateMachine.instance.ChangeState(state);

            yield return null;

            //Assert
            Assert.AreEqual(state, currentState);
        }
    }
}

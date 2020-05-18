using System.Collections;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GoalsTest
    {
        // example for mocking interfaces 
        [Test]
        public void WhenGoalCompleted_ShouldDequeueGoal()
        {
            // Arrange
            // mock character and goal
            Mock<Character> mockCharacter = new Mock<Character>(MockBehavior.Strict);
            Mock<IGoal> mockGoal = new Mock<IGoal>(MockBehavior.Strict);
            mockGoal.Setup(goal =>  goal.Process())
                         .Returns(GoalStatus.Completed);
            mockGoal.Setup(goal =>  goal.Status)
                    .Returns(GoalStatus.Completed);
            Think think = new Think(mockCharacter.Object);
            
            // Act
            think.AddSubGoal(mockGoal.Object);
            // procces could remove the completed mockgoal
            think.Process();
            
            // Assert
            // check if the queue is now empty
            Assert.AreEqual( new Queue(), think.subGoals);
        }

        [Test]
        public void WhenGoalAdded_ShouldAddToQueue()
        {
            // Arrange
            Mock<Character> character = new Mock<Character>(MockBehavior.Strict);
            Think think = new Think(character.Object);
            IGoal goal = new Sleep(character.Object);
            think.AddSubGoal(goal);

            // Act
            think.AddSubGoal(goal);
            think.AddSubGoal(goal);

            // Assert
            Assert.AreEqual(think.subGoals.Count, 3);
        }

        [Test]
        public void WhenGoalAdded_ShouldPrioritize() 
        {
            // Arrange
            Mock<Character> character = new Mock<Character>(MockBehavior.Strict);
            Think think = new Think(character.Object);
            IGoal goal = new Sleep(character.Object);
            think.AddSubGoal(goal);
            think.AddSubGoal(goal);
            think.AddSubGoal(goal);

            // Act
            think.PrioritizeSubGoal(goal);

            // Assert
            Assert.AreEqual(think.subGoals.Count, 1);
        }

    }
}

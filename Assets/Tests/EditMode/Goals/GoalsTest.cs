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
        [Test]
        public void WhenGoalCompleted_ShouldDequeueGoal()
        {
            // Arrange
            Mock<Character> mockCharacter = new Mock<Character>(MockBehavior.Strict);
            Mock<IGoal> mockGoal = new Mock<IGoal>(MockBehavior.Strict);
            mockGoal.Setup(goal => goal.Process()).Returns(GoalStatus.Completed);
            mockGoal.Setup(goal => goal.Status).Returns(GoalStatus.Completed);
            Think think = new Think(mockCharacter.Object);

            // Act
            think.AddSubGoal(mockGoal.Object);
            think.Process();

            // Assert
            Assert.AreEqual(new Queue(), think.SubGoals);
        }

        [Test]
        public void WhenRemoveCompletedSubgoals_ShouldRemoveCompletedSubgoals()
        {
            IGoal mockGoal1 = Mock.Of<IGoal>(mGoal => mGoal.Status == GoalStatus.Completed);
            IGoal mockGoal2 = Mock.Of<IGoal>(mGoal => mGoal.Status == GoalStatus.Active);
            IGoal mockGoal3 = Mock.Of<IGoal>(mGoal => mGoal.Status == GoalStatus.Inactive);

            // Arrange
            Queue<IGoal> goalQueue = new Queue<IGoal>();
            goalQueue.Enqueue(mockGoal1);
            goalQueue.Enqueue(mockGoal2);
            goalQueue.Enqueue(mockGoal3);

            // Act
            CompositeGoal.RemoveCompletedSubgoals(goalQueue);
            int actual = goalQueue.Count;

            // Assert
            Assert.AreEqual(2, actual);
        }

        [TestCase(GoalStatus.Completed, GoalStatus.Completed)]
        [TestCase(GoalStatus.Active, GoalStatus.Active)]
        [TestCase(GoalStatus.Failed, GoalStatus.Completed)]
        public void WhenProcessSubgoalsAndCompleted_ShouldBeCorrect(GoalStatus initial, GoalStatus expected)
        {
            IGoal mockGoal1 = Mock.Of<IGoal>(mGoal => mGoal.Status == initial);

            // Arrange
            Queue<IGoal> goalQueue = new Queue<IGoal>();
            goalQueue.Enqueue(mockGoal1);

            // Act
            GoalStatus actual = CompositeGoal.ProcessSubgoals(goalQueue);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void WhenGoalAdded_ShouldAddToQueue()
        {
            // Arrange
            Mock<Character> character = new Mock<Character>(MockBehavior.Strict);
            Think think = new Think(character.Object);
            IGoal goal = new Sleep(character.Object, Vector3.zero);
            think.AddSubGoal(goal);

            // Act
            think.AddSubGoal(goal);
            think.AddSubGoal(goal);

            // Assert
            Assert.AreEqual(think.SubGoals.Count, 3);
        }

        [Test]
        public void WhenGoalAdded_ShouldPrioritize()
        {
            // Arrange
            Mock<Character> character = new Mock<Character>(MockBehavior.Strict);
            Think think = new Think(character.Object);
            IGoal goal = new Sleep(character.Object, Vector3.zero);
            think.AddSubGoal(goal);
            think.AddSubGoal(goal);
            think.AddSubGoal(goal);

            // Act
            think.PrioritizeSubGoal(goal);

            // Assert
            Assert.AreEqual(think.SubGoals.Count, 1);
        }
    }
}
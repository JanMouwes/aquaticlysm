using System.Collections;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Resources;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class BoatGoalsTest
    {

        [Test]
        public void WhenCountingCarriedResources_ShouldReturnAmountOfAllResources()
        {
            // Arrange
            Mock<Boat> mockBoat = new Mock<Boat>(MockBehavior.Strict);

            // Act
            mockBoat.Object.CarriedResources = new Dictionary<string, float>();
            mockBoat.Object.CarriedResources.Add("fish", 5f);
            mockBoat.Object.CarriedResources.Add("wood", 10f);
            mockBoat.Object.CarriedResources.Add("metal", 5f);

            //Assert
            Assert.AreEqual(mockBoat.Object.CountResourcesCarried(), 20f);
        }

        [Test]
        public void WhenTryGetInvalidResourceValue_ShouldReturnZero()
        {
            // Arrange
            Mock<Boat> mockBoat = new Mock<Boat>(MockBehavior.Strict);

            //Act
            mockBoat.Object.CarriedResources = new Dictionary<string, float>();
            mockBoat.Object.CarriedResources.Add("fish", 5f);
            mockBoat.Object.CarriedResources.Add("wood", 10f);
            mockBoat.Object.CarriedResources.Add("metal", 5f);

            Assert.AreEqual(mockBoat.Object.TryGetResourceValue("food"), 0f);
        }

        [Test]
        public void WhenTryGetCorrectResource_ShouldReturnValue()
        {
            // Arrange
            Mock<Boat> mockBoat = new Mock<Boat>(MockBehavior.Strict);

            //Act
            mockBoat.Object.CarriedResources = new Dictionary<string, float>();
            mockBoat.Object.CarriedResources.Add("fish", 5f);
            mockBoat.Object.CarriedResources.Add("wood", 10f);
            mockBoat.Object.CarriedResources.Add("metal", 5f);

            Assert.AreEqual(mockBoat.Object.TryGetResourceValue("fish"), 5f);
        }

        [Test]
        public void WhenGetRandomOutermostEdgeVector3_ShouldReturnVector3() 
        {
            //Arrange
            Vector3 point;

            //Act
            point = Expedition.GetRandomOutermostEdgeVector3(30);

            //Assert
            Assert.IsTrue(point.x == 15 || point.x == -15 || point.z == 15 || point.z == -15);
        }
    }
}

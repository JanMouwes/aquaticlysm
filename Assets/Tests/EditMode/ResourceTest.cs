using System.Collections;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ResourceTest
    {
        [SetUp]
        public void SetUp()
        {
            ResourceManager.Instance.Clear();    
        }

        // [TearDown]
        // private void TearDown()
        // {
        //     
        // }
        
        [Test]
        public void WhenResourceGathered_ShouldContainResource()
        {
            //Arrange
            ResourceManager instance = ResourceManager.Instance;
            
            //Act
            instance.GatherResource("wood", 12);
            
            //Assert
            Assert.AreEqual(12, instance.GetResourceAmount("wood"));
        }

        [TestCase(12, 4, 8, true)]
        [TestCase(12, -13, 8, true)]
        public void WhenResourceUsed_ShouldRetractResource(int startingAmount,int useAmount,  int expected, bool expectedBool)
        {
            //Arrange
            ResourceManager instance = ResourceManager.Instance;
            instance.AddNewResource("wood", startingAmount);
            
            //Act
            bool actual = instance.UseResource("wood", useAmount);
            int actualAmount = instance.GetResourceAmount("wood");

            //Assert
            Assert.AreEqual(expectedBool, actual);
            Assert.AreEqual(expected, actualAmount);
        }
        
        // // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // // `yield return null;` to skip a frame.
        // [UnityTest]
        // public IEnumerator ResourceTestWithEnumeratorPasses()
        // {
        //     // Use the Assert class to test conditions.
        //     // Use yield to skip a frame.
        //     yield return null;
        // }
    }
}

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
        [TestCase(12, -13, 12, false)]
        [TestCase(12, 13, 12, false)]
        [TestCase(12, 0, 12, true)]
        [TestCase(12, 12, 0, true)]
        public void WhenResourceUsed_ShouldRetractResource(int startingAmount,int useAmount,  int expected, bool expectedBool)
        {
            //Arrange
            ResourceManager instance = ResourceManager.Instance;
            instance.AddResourceType("wood", startingAmount);
            
            //Act
            bool actual = instance.UseResource("wood", useAmount);
            int actualAmount = instance.GetResourceAmount("wood");

            //Assert
            Assert.AreEqual(expected, actualAmount);
            Assert.AreEqual(expectedBool, actual);
        }
        
        [Test]
        public void WhenMultipleSameTypeResourceGathered_ShouldContainResource()
        {
            //Arrange
            ResourceManager instance = ResourceManager.Instance;
            
            //Act
            instance.GatherResource("wood", 12);
            instance.GatherResource("wood", 12);
            
            //Assert
            Assert.AreEqual(24, instance.GetResourceAmount("wood"));
        }
        
        [Test]
        public void WhenResourceGatheredAndAdded_ShouldContainResource()
        {
            //Arrange
            ResourceManager instance = ResourceManager.Instance;
            
            //Act
            instance.GatherResource("wood", 12);
            instance.AddResourceType("wood", 12);
            
            //Assert
            Assert.AreEqual(12, instance.GetResourceAmount("wood"));
        }
        
        [Test]
        public void WhenDifferentResourceGatheredAndAdded_ShouldContainResource()
        {
            //Arrange
            ResourceManager instance = ResourceManager.Instance;
            
            //Act
            instance.GatherResource("wood", 12);
            instance.AddResourceType("steel", 8);
            
            //Assert
            Assert.AreEqual(12, instance.GetResourceAmount("wood"));
            Assert.AreEqual(8, instance.GetResourceAmount("steel"));
        }
        
        [Test]
        public void WhenResourceRemoved_ShouldnotContainResource()
        {
            //Arrange
            ResourceManager instance = ResourceManager.Instance;
            bool expected = false;
            
            //Act
            instance.AddResourceType("steel", 8);
            instance.RemoveResourceType("steel");
            bool actual = instance.DoesResourceExist("steel");
            
            
            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}

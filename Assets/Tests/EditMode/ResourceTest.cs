using Moq;
using NUnit.Framework;
using Resources;

namespace Tests.EditMode
{
    public class ResourceTest
    {
        private IResourceTypeManager _resourceTypeManager;
        private ResourceManager _resourceManager;

        [SetUp]
        public void SetUp()
        {
            Mock<IResourceTypeManager> managerMock = new Mock<IResourceTypeManager>();
            ResourceType type;
            managerMock.Setup(manager => manager.TryGetResourceType(It.IsAny<string>(), out type))
                       .Callback(() => { type.ShortName = "wood"; })
                       .Returns(true);

            this._resourceTypeManager = managerMock.Object;

            // Reset resource manager
            ResourceManager.Instance.Clear();
            this._resourceManager = ResourceManager.Instance;
            this._resourceManager.SetResourceTypeManager(this._resourceTypeManager);
        }

        [Test]
        public void WhenResourceGathered_ShouldContainResource()
        {
            //Arrange

            //Act
            this._resourceManager.IncreaseResource("wood", 12);

            //Assert
            Assert.AreEqual(12, this._resourceManager.GetResourceAmount("wood"));
        }

        [TestCase(12, 4, 8, true)]
        [TestCase(12, -13, 12, false)]
        [TestCase(12, 13, 12, false)]
        [TestCase(12, 0, 12, true)]
        [TestCase(12, 12, 0, true)]
        public void WhenResourceUsed_ShouldRetractResource(int startingAmount, int useAmount, int expected, bool expectedBool)
        {
            //Arrange
            ResourceManager instance = this._resourceManager;
            instance.AddResourceType("wood", startingAmount);

            //Act
            bool actual = instance.DecreaseResource("wood", useAmount);
            int actualAmount = instance.GetResourceAmount("wood");

            //Assert
            Assert.AreEqual(expected, actualAmount);
            Assert.AreEqual(expectedBool, actual);
        }

        [Test]
        public void WhenMultipleSameTypeResourceGathered_ShouldContainResource()
        {
            //Arrange
            ResourceManager instance = this._resourceManager;

            //Act
            instance.IncreaseResource("wood", 12);
            instance.IncreaseResource("wood", 12);

            //Assert
            Assert.AreEqual(24, instance.GetResourceAmount("wood"));
        }

        [Test]
        public void WhenResourceGatheredAndAdded_ShouldContainResource()
        {
            //Arrange
            ResourceManager instance = this._resourceManager;

            //Act
            instance.IncreaseResource("wood", 12);
            instance.AddResourceType("wood", 12);

            //Assert
            Assert.AreEqual(12, instance.GetResourceAmount("wood"));
        }

        [Test]
        public void WhenDifferentResourceGatheredAndAdded_ShouldContainResource()
        {
            //Arrange
            ResourceManager instance = this._resourceManager;

            //Act
            instance.IncreaseResource("wood", 12);
            instance.AddResourceType("steel", 8);

            //Assert
            Assert.AreEqual(12, instance.GetResourceAmount("wood"));
            Assert.AreEqual(8, instance.GetResourceAmount("steel"));
        }

        [Test]
        public void WhenResourceRemoved_ShouldnotContainResource()
        {
            //Arrange
            ResourceManager instance = this._resourceManager;
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
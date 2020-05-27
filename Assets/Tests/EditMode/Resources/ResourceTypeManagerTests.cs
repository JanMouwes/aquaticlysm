using System.Linq;
using System.Xml;
using NUnit.Framework;
using Resources;

namespace Tests.EditMode.Resources
{
    [TestFixture]
    public class ResourceTypeManagerTests
    {
        [Test]
        public void Test_WhenParseResourcesFromXmlFileCalled_ShouldReturnResources()
        {
            // Arrange
            const string ownPath = "./Assets/Tests/EditMode/Resources/";
            const string workingTestFile = ownPath + "TestFiles/resources.xml";
        
            // Act
            ResourceType[] resourceTypes = ResourceTypeManager.ParseResourcesFromXmlFile(workingTestFile)
                                                              .ToArray();
        
            // Assert
            Assert.AreEqual(4, resourceTypes.Length);
        }
        
        [Test]
        public void Test_WhenParseResourcesFromXmlFileCalledWithIncorrectFile_ShouldThrowException()
        {
            // Arrange
            const string ownPath = "./Assets/Tests/EditMode/Resources/";
            const string brokenTestFile = ownPath + "TestFiles/resources-incorrect.xml";
        
            // Act
        
            // Assert
            Assert.Throws<XmlException>(() =>
            {
                ResourceTypeManager.ParseResourcesFromXmlFile(brokenTestFile)
                                   .ToArray();
            });
        }
        [Test]
        public void Test_WhenParseResourceStartingValues_ShouldReturnResources()
        {
            // Arrange
            const string ownPath = "./Assets/Tests/EditMode/Resources/";
            const string workingTestFile = ownPath + "TestFiles/starting-values.xml";
        
            // Act
            ResourceType[] resourceTypes = ResourceTypeManager.ParseResourcesFromXmlFile(workingTestFile)
                                                              .ToArray();
        
            // Assert
            Assert.AreEqual(4, resourceTypes.Length);
        }
        
        [Test]
        public void Test_WhenParseResourceStartingValues_ShouldThrowException()
        {
            // Arrange
            const string ownPath = "./Assets/Tests/EditMode/Resources/";
            const string brokenTestFile = ownPath + "TestFiles/starting-values-incorrect.xml";
        
            // Act
        
            // Assert
            Assert.Throws<XmlException>(() =>
            {
                ResourceTypeManager.ParseResourcesFromXmlFile(brokenTestFile)
                                   .ToArray();
            });
        }
    }
}
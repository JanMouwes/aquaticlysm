using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class NoiseGeneratorTests
    {
        [Test]
        public void GenerateNoiseChangesVertices()
        {
            NoiseGenerator noiseGenerator = new NoiseGenerator();

            //Arrange
            Vector3[] values = new Vector3[] 
            {
                new Vector3(0,0,0),
                new Vector3(10,0,10)
            };

            //Act
            Vector3[] newValues = GenerateNoise(values);

            //Assert
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator NewTestScriptWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}

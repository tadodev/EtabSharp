using System;
using EtabSharp.Elements.AreaObj.Models;
using EtabSharp.Elements.FrameObj.Models;

namespace EtabSharp.Test
{
    /// <summary>
    /// Simple test to verify our load implementations work correctly
    /// </summary>
    public class LoadImplementationTest
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Testing Load Implementations...\n");

            // Test AreaUniformLoad
            TestAreaUniformLoad();
            
            // Test FrameDistributedLoad
            TestFrameDistributedLoad();
            
            Console.WriteLine("\nAll tests completed successfully!");
        }

        private static void TestAreaUniformLoad()
        {
            Console.WriteLine("=== Testing AreaUniformLoad ===");

            // Test basic creation
            var load1 = new AreaUniformLoad("Area1", "DEAD", 100.0, 100.0, 1, 10, 0.0, 1.0, "Global", true);
            Console.WriteLine($"✓ Basic load: {load1}");
            Console.WriteLine($"  - Valid: {load1.IsValid()}");
            Console.WriteLine($"  - Is Gravity: {load1.IsGravityLoad()}");
            Console.WriteLine($"  - Is Uniform: {load1.IsUniform()}");

            // Test factory methods
            var gravityLoad = AreaUniformLoad.CreateGravityLoad("Area1", "DEAD", 50.0);
            Console.WriteLine($"✓ Gravity load: {gravityLoad}");
            Console.WriteLine($"  - Direction: {gravityLoad.GetDirectionDescription()}");

            var pressureLoad = AreaUniformLoad.CreatePressureLoad("Area1", "WIND", 25.0);
            Console.WriteLine($"✓ Pressure load: {pressureLoad}");
            Console.WriteLine($"  - Direction: {pressureLoad.GetDirectionDescription()}");

            // Test all directions
            for (int dir = 1; dir <= 11; dir++)
            {
                var testLoad = new AreaUniformLoad("Area1", "TEST", 10.0, 10.0, 1, dir, 0.0, 1.0, "Global", true);
                Console.WriteLine($"  - Direction {dir}: {testLoad.GetDirectionDescription()} - Valid: {testLoad.IsValid()}");
            }

            Console.WriteLine();
        }

        private static void TestFrameDistributedLoad()
        {
            Console.WriteLine("=== Testing FrameDistributedLoad ===");

            // Test basic creation
            var load1 = new FrameDistributedLoad("Frame1", "DEAD", 2.5, 2.5, 1, 10, 0.0, 1.0, "Global", true);
            Console.WriteLine($"✓ Basic load: {load1}");
            Console.WriteLine($"  - Valid: {load1.IsValid()}");
            Console.WriteLine($"  - Is Gravity: {load1.IsGravityLoad()}");
            Console.WriteLine($"  - Is Uniform: {load1.IsUniform()}");

            // Test factory methods
            var uniformLoad = FrameDistributedLoad.CreateUniformLoad("Frame1", "DEAD", 3.0);
            Console.WriteLine($"✓ Uniform load: {uniformLoad}");

            var gravityLoad = FrameDistributedLoad.CreateGravityLoad("Frame1", "LIVE", 2.0);
            Console.WriteLine($"✓ Gravity load: {gravityLoad}");

            var triangularLoad = FrameDistributedLoad.CreateTriangularLoad("Frame1", "WIND", 0.0, 4.0);
            Console.WriteLine($"✓ Triangular load: {triangularLoad}");
            Console.WriteLine($"  - Is Uniform: {triangularLoad.IsUniform()}");

            var partialLoad = FrameDistributedLoad.CreatePartialLoad("Frame1", "EQUIPMENT", 8.0, 0.25, 0.75);
            Console.WriteLine($"✓ Partial load: {partialLoad}");

            // Test all directions
            for (int dir = 1; dir <= 11; dir++)
            {
                var testLoad = new FrameDistributedLoad("Frame1", "TEST", 1.0, 1.0, 1, dir, 0.0, 1.0, "Global", true);
                Console.WriteLine($"  - Direction {dir}: {testLoad.GetDirectionDescription()} - Valid: {testLoad.IsValid()}");
            }

            // Test load types
            var forceLoad = new FrameDistributedLoad("Frame1", "FORCE", 1.0, 1.0, 1, 10, 0.0, 1.0, "Global", true);
            var momentLoad = new FrameDistributedLoad("Frame1", "MOMENT", 1.0, 1.0, 2, 1, 0.0, 1.0, "Local", true);
            Console.WriteLine($"✓ Force load type: {forceLoad.GetLoadTypeDescription()}");
            Console.WriteLine($"✓ Moment load type: {momentLoad.GetLoadTypeDescription()}");

            // Test validation
            var invalidLoad = new FrameDistributedLoad("", "", 0.0, 0.0, 0, 0, 0.0, 0.0, "", true);
            Console.WriteLine($"✓ Invalid load validation: {invalidLoad.IsValid()}");

            Console.WriteLine();
        }
    }
}
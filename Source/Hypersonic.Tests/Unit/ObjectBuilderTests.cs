using System;
using System.Collections.Generic;
using FakeItEasy;
using Hypersonic.Core;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Hypersonic.Tests.Unit
{
    [TestFixture]
    public class ObjectBuilderTests
    {
        [Test]
        public void ObjectBuilder_SingleLevel_ObjectSuccessfullyPopulated()
        {
            IHypersonicDbReader reader = A.Fake<IHypersonicDbReader>();
            Fixture fixture = new Fixture();
            Guid test = fixture.CreateAnonymous<Guid>();

            A.CallTo(() => reader.GetValue(A<string>.Ignored)).Returns(test);

            ObjectBuilder objectBuilder = new ObjectBuilder();
            SingleLevel level = objectBuilder.HydrateType<SingleLevel>(reader);

            Assert.IsNotNull(level);
        }

        public class SingleLevel
        {
            public Guid Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        [Test]
        public void ObjectBuilder_NullableInt_ValueOfOneIsSetToNullableType()
        {
            IHypersonicDbReader reader = A.Fake<IHypersonicDbReader>();

            int test = 1;
            Console.WriteLine("Test Value: {0}", test);

            A.CallTo(() => reader.GetValue(A<string>.Ignored)).Returns(test);

            ObjectBuilder objectBuilder = new ObjectBuilder();
            NullableInt testClass = objectBuilder.HydrateType<NullableInt>(reader);

            Console.WriteLine("Actual Value: {0}", test);
            Assert.AreEqual(test, testClass.Id, "Expected Property to have the same value as Test data");
        }

        [Test]
        public void ObjectBuilder_NullableInt_ValueOfNullIsSetToNullableType()
        {
            IHypersonicDbReader reader = A.Fake<IHypersonicDbReader>();

            int? test = null;
            Console.WriteLine("Test Value: {0}", test);

            A.CallTo(() => reader.GetValue(A<string>.Ignored)).Returns(test);

            ObjectBuilder objectBuilder = new ObjectBuilder();
            NullableInt testClass = objectBuilder.HydrateType<NullableInt>(reader);

            Console.WriteLine("Actual Value: {0}", test);
            Assert.AreEqual(test, testClass.Id, "Expected Property to have the same value as Test data");
        }

        public class NullableInt
        {
            public int? Id { get; set; }
        }

        [Test]
        public void ObjectBuilder_NullableGuid_ValueOfGuidIsSetToNullableType()
        {
            IHypersonicDbReader reader = A.Fake<IHypersonicDbReader>();

            Guid test = Guid.NewGuid();
            Console.WriteLine("Test Value: {0}", test);

            A.CallTo(() => reader.GetValue(A<string>.Ignored)).Returns(test);

            ObjectBuilder objectBuilder = new ObjectBuilder();
            NullableGuid testClass = objectBuilder.HydrateType<NullableGuid>(reader);

            Console.WriteLine("Actual Value: {0}", test);
            Assert.AreEqual(test, testClass.Id, "Expected Property to have the same value as Test data");
        }

        [Test]
        public void ObjectBuilder_NullableGuid_ValueOfNullIsSetToNullableType()
        {
            IHypersonicDbReader reader = A.Fake<IHypersonicDbReader>();

            Guid? test = null;
            Console.WriteLine("Test Value: {0}", test);

            A.CallTo(() => reader.GetValue(A<string>.Ignored)).Returns(test);

            ObjectBuilder objectBuilder = new ObjectBuilder();
            NullableGuid testClass = objectBuilder.HydrateType<NullableGuid>(reader);

            Console.WriteLine("Actual Value: {0}", test);
            Assert.AreEqual(test, testClass.Id, "Expected Property to have the same value as Test data");
        }

        public class NullableGuid
        {
            public Guid? Id { get; set; }
        }
        
        [Test]
        public void ObjectBuilder_Enums_EnumValuesWithIntsHaveThereValuesSet()
        {
            IHypersonicDbReader reader = A.Fake<IHypersonicDbReader>();
            const string test = "Val1";
            Console.WriteLine("Test Value: {0}", test);

            A.CallTo(() => reader.GetValue(A<string>.Ignored)).Returns(test);

            ObjectBuilder objectBuilder = new ObjectBuilder();
            EnumClass testClass = objectBuilder.HydrateType<EnumClass>(reader);

            Console.WriteLine("Actual Value: {0}", testClass.Enum.ToString());
            StringAssert.AreEqualIgnoringCase(test, testClass.Enum.ToString(), "Expected Enums to have the same value.");
        }
        
        [Test]
        public void ObjectBuilder_SecondLevel_ValuesAreCastedAndPropertiesArePopulated()
        {
            IHypersonicDbReader reader = A.Fake<IHypersonicDbReader>();
            Guid val = Guid.NewGuid();
            Console.WriteLine("Test Value: {0}", val);

            A.CallTo(() => reader.GetValue(A<string>._)).Returns(val);
            ObjectBuilder objectBuilder = new ObjectBuilder();
            SecondLevel testClass = objectBuilder.HydrateType<SecondLevel>(reader);

            Console.WriteLine("Actual Value: {0}", testClass.SingleLevel.Id);
            Assert.AreEqual(val, testClass.SingleLevel.Id, "Expected string values to have the same value.");
        }

        public object Name(string name)
        {
            return "Chuck";
        }

        public class EnumClass
        {
            public MyEnum Enum { get; set; }
        }

        public enum MyEnum
        {
            Val1,
            Val2,
            Val3
        }

        public class SecondLevel
        {
            public SingleLevel SingleLevel { get; set; }
            public string UpdateDate { get; set; }
        }

        [Test]
        public void ObjectBuilder_PrivateProperties_PrivatePropertiesAreNotSet()
        {
            IHypersonicDbReader reader = A.Fake<IHypersonicDbReader>();
            string val = Guid.NewGuid().ToString();
            Console.WriteLine("Test Value: {0}", val);

            A.CallTo(() => reader.GetValue(A<string>._)).Returns(val);
            ObjectBuilder objectBuilder = new ObjectBuilder();
            PrivateProperties testClass = objectBuilder.HydrateType<PrivateProperties>(reader);

            //Console.WriteLine("Actual Value: {0}", testClass.SingleLevel.Id);
            Assert.IsTrue(testClass.PropertyIsNull(), "Expected the private property to be null");
        }

        public class PrivateProperties
        {
            private string DontSetMe { get; set; }
            public bool PropertyIsNull()
            {
                return string.IsNullOrEmpty(DontSetMe);
            }
        }

        [Test]
        public void ObjectBuilder_CollectionsProperties_CollectionsAreNotSet()
        {
            IHypersonicDbReader reader = A.Fake<IHypersonicDbReader>();
            string val = Guid.NewGuid().ToString();
            Console.WriteLine("Test Value: {0}", val);

            A.CallTo(() => reader.GetValue(A<string>._)).Returns(val);
            ObjectBuilder objectBuilder = new ObjectBuilder();
            CollectionClass testClass = objectBuilder.HydrateType<CollectionClass>(reader);

            Assert.IsTrue(testClass.PropertyIsNull(), "Expected the private property to be null");
        }

        public class CollectionClass
        {
            private List<string> DontSetMe { get; set; }
            public bool PropertyIsNull()
            {
                return DontSetMe == null;
            }
        }
    }
}

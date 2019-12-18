using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using UDPMessaging.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UDPMessaging.Exceptions;
using UDPMessaging.Identification.MessageVersionIdentification;
using UDPMessaging.Identification.PeerIdentification;
using UDPMessaging.Serialisation;
using NUnit;
// ReSharper disable InconsistentNaming

namespace UDPMessaging.ISerializable.Tests.Serialisation
{
    [TestClass]
    public class ISerializerTests
    {
        internal class ISerializerTestsData
        {
            private static readonly Faker Faker = new Faker();

            public static IBaseMessage GenerateMessage()
            {
                return new StringMessage()
                {
                    Data = Faker.Random.Word(),
                    From = new StringPeerIdentification(Faker.Random.Word()),
                    To = new StringPeerIdentification(Faker.Random.Word()),
                    Version = new IntMessageVersionIdentification(Faker.Random.Int())
                };
            }
        }

        [TestMethod]
        public void BinaryFormatter_Serialisation_DoesntThrow()
        {
            IBaseMessage message = ISerializerTestsData.GenerateMessage();
            ISerializer binaryFormatter = new BinaryFormatter();

            binaryFormatter.Serialize(message);
        }

        [TestMethod]
        public void BinaryFormatter_Serialisation_ThrowsOnNull()
        {
            ISerializer binaryFormatter = new BinaryFormatter();

            Assert.ThrowsException<ArgumentNullException>(() => binaryFormatter.Serialize(null));
        }

        [TestMethod]
        public void BinaryFormatter_Deserialisation_DoesntThrow()
        {
            IBaseMessage message = ISerializerTestsData.GenerateMessage();
            ISerializer binaryFormatter = new BinaryFormatter();
            byte[] messageBytes = binaryFormatter.Serialize(message);

            IBaseMessage messageDeserialized = binaryFormatter.Deserialize(messageBytes);

            Assert.AreEqual(message.To, messageDeserialized.To);
            Assert.AreEqual(message.From, messageDeserialized.From);
            Assert.AreEqual(message.Type, messageDeserialized.Type);
            Assert.AreEqual(message.Version, messageDeserialized.Version);
        }

        [TestMethod]
        public void BinaryFormatter_Deserialisation_ThrowsOnNull()
        {
            ISerializer binaryFormatter = new BinaryFormatter();

            Assert.ThrowsException<ArgumentNullException>(() => binaryFormatter.Deserialize(null));
        }

        [TestMethod]
        public void JSONSerialiser_Serialisation_DoesntThrowOnValidData()
        {
            IBaseMessage message = ISerializerTestsData.GenerateMessage();
            ISerializer jsonSerialiser = new JSONSerialiser();

            jsonSerialiser.Serialize(message);
        }

        [TestMethod]
        public void JSONSerialiser_Serialisation_ThrowsOnNull()
        {
            ISerializer jsonSerialiser = new JSONSerialiser();

            Assert.ThrowsException<ArgumentNullException>(() => jsonSerialiser.Serialize(null));
        }

        [TestMethod]
        public void JSONSerialiser_Deserialisation_DoesntThrowOnValidData()
        {
            IBaseMessage message = ISerializerTestsData.GenerateMessage();
            ISerializer jsonSerialiser = new JSONSerialiser();
            byte[] messageBytes = jsonSerialiser.Serialize(message);

            IBaseMessage messageDeserialized = jsonSerialiser.Deserialize(messageBytes);

            Assert.AreEqual(message.To, messageDeserialized.To);
            Assert.AreEqual(message.From, messageDeserialized.From);
            Assert.AreEqual(message.Type, messageDeserialized.Type);
            Assert.AreEqual(message.Version, messageDeserialized.Version);
        }

        [TestMethod]
        public void JSONSerialiser_Deserialisation_ThrowsOnNull()
        {
            ISerializer jsonSerialiser = new JSONSerialiser();

            Assert.ThrowsException<ArgumentNullException>(() => jsonSerialiser.Deserialize(null));
        }
    }
}

using System.Net;
using Chess.Multiplayer.Socket;
using Moq;
using NUnit.Framework;

namespace Chess.Multiplayer.Test
{
    [TestFixture]
    public class ClientTests
    {
        private Client _client;
        private Mock<ISocket> _socketMock;

        [SetUp]
        public void SetUp()
        {
            _socketMock = new Mock<ISocket>();

            _client = new Client(_socketMock.Object);
        }

        [Test]
        public void Connect_DeveChamarClientConnectUmaVez()
        {
            _client.Connect(It.IsAny<IPAddress>(), It.IsAny<int>());

            _socketMock.Verify(m => m.Connect(It.IsAny<IPAddress>(), It.IsAny<int>()), Times.Once);
        }
    }
}

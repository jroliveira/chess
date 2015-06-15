using System.Net;
using Chess.Multiplayer.Socket;
using Moq;
using NUnit.Framework;

namespace Chess.Multiplayer.Test
{
    [TestFixture]
    public class ServerTests
    {
        private Server _server;
        private Mock<ISocket> _socketMock;

        [SetUp]
        public void SetUp()
        {
            _socketMock = new Mock<ISocket>();

            _server = new Server(_socketMock.Object);
        }

        [Test]
        public void Listen_DeveChamarServerBindUmaVez()
        {
            _server.Listen();

            _socketMock.Verify(m => m.Bind(It.IsAny<IPAddress>(), It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Listen_DeveChamarServerListenUmaVez()
        {
            _server.Listen();

            _socketMock.Verify(m => m.Listen(), Times.Once);
        }
    }
}

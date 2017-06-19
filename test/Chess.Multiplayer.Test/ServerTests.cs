namespace Chess.Multiplayer.Test
{
    using System.Net;

    using Chess.Multiplayer.Socket;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class ServerTests
    {
        private Server server;
        private Mock<ISocket> socketMock;

        [SetUp]
        public void SetUp()
        {
            this.socketMock = new Mock<ISocket>();

            this.server = new Server(this.socketMock.Object);
        }

        [Test]
        public void Listen_DeveChamarServerBindUmaVez()
        {
            this.server.Listen();

            this.socketMock.Verify(m => m.Bind(It.IsAny<IPAddress>(), It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Listen_DeveChamarServerListenUmaVez()
        {
            this.server.Listen();

            this.socketMock.Verify(m => m.Listen(), Times.Once);
        }
    }
}

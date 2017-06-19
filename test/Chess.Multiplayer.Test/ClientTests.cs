namespace Chess.Multiplayer.Test
{
    using System.Net;

    using Chess.Multiplayer.Socket;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class ClientTests
    {
        private Client client;
        private Mock<ISocket> socketMock;

        [SetUp]
        public void SetUp()
        {
            this.socketMock = new Mock<ISocket>();

            this.client = new Client(this.socketMock.Object);
        }

        [Test]
        public void Connect_DeveChamarClientConnectUmaVez()
        {
            this.client.Connect(It.IsAny<IPAddress>(), It.IsAny<int>());

            this.socketMock.Verify(m => m.Connect(It.IsAny<IPAddress>(), It.IsAny<int>()), Times.Once);
        }
    }
}

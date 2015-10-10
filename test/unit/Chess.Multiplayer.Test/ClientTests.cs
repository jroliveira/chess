namespace Chess.Multiplayer.Test
{
    using System.Net;

    using Chess.Multiplayer.Socket;

    using Moq;

    using Xunit;

    public class ClientTests
    {
        private readonly Client client;
        private readonly Mock<ISocket> socketMock;

        public ClientTests()
        {
            this.socketMock = new Mock<ISocket>();

            this.client = new Client(this.socketMock.Object);
        }

        [Fact]
        public void Connect_DeveChamarClientConnectUmaVez()
        {
            this.client.Connect(It.IsAny<IPAddress>(), It.IsAny<int>());

            this.socketMock.Verify(m => m.Connect(It.IsAny<IPAddress>(), It.IsAny<int>()), Times.Once);
        }
    }
}

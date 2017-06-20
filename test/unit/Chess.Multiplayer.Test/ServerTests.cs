namespace Chess.Multiplayer.Test
{
    using System.Net;

    using Chess.Multiplayer.Socket;

    using Moq;

    using Xunit;

    public class ServerTests
    {
        private readonly Server server;
        private readonly Mock<ISocket> socketMock;

        public ServerTests()
        {
            this.socketMock = new Mock<ISocket>();

            this.server = new Server(this.socketMock.Object);
        }

        [Fact]
        public void Listen_DeveChamarServerBindUmaVez()
        {
            this.server.Listen();

            this.socketMock.Verify(m => m.Bind(It.IsAny<IPAddress>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void Listen_DeveChamarServerListenUmaVez()
        {
            this.server.Listen();

            this.socketMock.Verify(m => m.Listen(), Times.Once);
        }
    }
}

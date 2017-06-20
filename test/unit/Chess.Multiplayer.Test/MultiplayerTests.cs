namespace Chess.Multiplayer.Test
{
    using Chess.Multiplayer.Socket;

    using Moq;

    using Xunit;

    public class MultiplayerTests
    {
        private readonly Multiplayer multiplayer;
        private readonly Mock<ISocket> socketMock;

        public MultiplayerTests()
        {
            this.socketMock = new Mock<ISocket>();


            this.multiplayer = new Multiplayer(this.socketMock.Object);
        }

        [Fact]
        public void SendTheMove_DadaPosicaoENovaPosicao_DeveChamarClientSendUmaVez()
        {
            this.multiplayer.SendTheMove("d6", "d7");

            this.socketMock.Verify(m => m.Send("d6->d7"), Times.Once());
        }

        [Fact]
        public void WaitingTheMove_DeveChamarClientReceiveUmaVez()
        {
            this.multiplayer.WaitingTheMove();

            this.socketMock.Verify(m => m.Receive(), Times.Once());
        }

        [Fact]
        public void Disconnect_DeveChamarClientShutdownUmaVez()
        {
            this.multiplayer.Disconnect();

            this.socketMock.Verify(m => m.Shutdown(), Times.Once());
        }
    }
}
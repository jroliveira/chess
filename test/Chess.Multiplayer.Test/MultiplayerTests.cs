namespace Chess.Multiplayer.Test
{
    using Chess.Multiplayer.Socket;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class MultiplayerTests
    {
        private Multiplayer multiplayer;
        private Mock<ISocket> socketMock;

        [SetUp]
        public void SetUp()
        {
            this.socketMock = new Mock<ISocket>();


            this.multiplayer = new Multiplayer(this.socketMock.Object);
        }

        [Test]
        public void SendTheMove_DadaPosicaoENovaPosicao_DeveChamarClientSendUmaVez()
        {
            this.multiplayer.SendTheMove("d6", "d7");

            this.socketMock.Verify(m => m.Send("d6->d7"), Times.Once());
        }

        [Test]
        public void WaitingTheMove_DeveChamarClientReceiveUmaVez()
        {
            this.multiplayer.WaitingTheMove();

            this.socketMock.Verify(m => m.Receive(), Times.Once());
        }

        [Test]
        public void Disconnect_DeveChamarClientShutdownUmaVez()
        {
            this.multiplayer.Disconnect();

            this.socketMock.Verify(m => m.Shutdown(), Times.Once());
        }

        [Test]
        public void Disconnect_DeveChamarClientCloseUmaVez()
        {
            this.multiplayer.Disconnect();

            this.socketMock.Verify(m => m.Close(), Times.Once());
        }
    }
}
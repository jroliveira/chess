using Chess.Multiplayer.Socket;
using Moq;
using NUnit.Framework;

namespace Chess.Multiplayer.Test
{
    [TestFixture]
    public class MultiplayerTests
    {
        private Multiplayer _multiplayer;
        private Mock<ISocket> _socketMock;

        [SetUp]
        public void SetUp()
        {
            _socketMock = new Mock<ISocket>();


            _multiplayer = new Multiplayer(_socketMock.Object);
        }

        [Test]
        public void SendTheMove_DadaPosicaoENovaPosicao_DeveChamarClientSendUmaVez()
        {
            _multiplayer.SendTheMove("d6", "d7");

            _socketMock.Verify(m => m.Send("d6->d7"), Times.Once());
        }

        [Test]
        public void WaitingTheMove_DeveChamarClientReceiveUmaVez()
        {
            _multiplayer.WaitingTheMove();

            _socketMock.Verify(m => m.Receive(), Times.Once());
        }

        [Test]
        public void Disconnect_DeveChamarClientShutdownUmaVez()
        {
            _multiplayer.Disconnect();

            _socketMock.Verify(m => m.Shutdown(), Times.Once());
        }

        [Test]
        public void Disconnect_DeveChamarClientCloseUmaVez()
        {
            _multiplayer.Disconnect();

            _socketMock.Verify(m => m.Close(), Times.Once());
        }
    }
}
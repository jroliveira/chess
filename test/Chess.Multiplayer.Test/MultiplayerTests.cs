using Chess.Multiplayer.Socket;
using Chess.Multiplayer.Test.Fakes;
using Moq;
using NUnit.Framework;

namespace Chess.Multiplayer.Test
{
    [TestFixture]
    public class MultiplayerTests
    {
        private Multiplayer _fakeMultiplayer;
        private Mock<ISocket> _socketMock;

        [SetUp]
        public void SetUp()
        {
            _socketMock = new Mock<ISocket>();

            _fakeMultiplayer = new FakeMultiplayer(_socketMock.Object);
        }

        [Test]
        public void SendTheMove_DadaPosicaoENovaPosicao_DeveChamarClientSendUmaVez()
        {
            _fakeMultiplayer.SendTheMove("d6", "d7");

            _socketMock.Verify(m => m.Send("d6->d7"), Times.Once());
        }

        [Test]
        public void WaitingTheMove_DeveChamarClientReceiveUmaVez()
        {
            _fakeMultiplayer.WaitingTheMove();

            _socketMock.Verify(m => m.Receive(), Times.Once());
        }

        [Test]
        public void Disconnect_DeveChamarClientShutdownUmaVez()
        {
            _fakeMultiplayer.Disconnect();

            _socketMock.Verify(m => m.Shutdown(), Times.Once());
        }

        [Test]
        public void Disconnect_DeveChamarClientCloseUmaVez()
        {
            _fakeMultiplayer.Disconnect();

            _socketMock.Verify(m => m.Close(), Times.Once());
        }
    }
}
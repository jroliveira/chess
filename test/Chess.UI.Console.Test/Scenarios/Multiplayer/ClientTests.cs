namespace Chess.UI.Console.Test.Scenarios.Multiplayer
{
    using Chess.UI.Console.Scenarios.Matches;
    using Chess.UI.Console.Scenarios.Multiplayer;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class ClientTests : ScenarioTests
    {
        private Mock<Online> matchMock;

        [SetUp]
        public new void Setup()
        {
            base.Setup();

            this.ReaderMock
                .SetupSequence(text => text.ReadValue())
                .Returns("127.0.0.1")
                .Returns("11000");

            this.matchMock = new Mock<Online>();
        }

        [Test]
        public void Start_DeveChamarWriteInsideTheBoxUmaVez()
        {
            this.Start();

            this.WriterMock.Verify(writer => writer.WriteInsideTheBox("connection data"), Times.Once);
        }

        [Test]
        public void Start_DeveChamarReadValueDuasVez()
        {
            this.Start();

            this.ReaderMock.Verify(reader => reader.ReadValue(), Times.Exactly(2));
        }

        [Test]
        public void Start_DadoUmaIpAddressEUmaPorta_DeveChamarGameConnectUmaVez()
        {
            this.ReaderMock
                .SetupSequence(text => text.ReadValue())
                .Returns("127.0.0.1")
                .Returns("11000");

            this.Start();

            this.GameMock.Verify(game => game.Connect("127.0.0.1", "11000"), Times.Once);
        }

        protected override void Start()
        {
            var client = new Client(this.GameMock.Object, this.matchMock.Object, this.WriterMock.Object, this.ReaderMock.Object, this.ScreenMock.Object);
            client.Start();
        }
    }
}
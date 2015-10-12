﻿using Chess.Validations;
using Chess.Validations.KnightValidations;
using Moq;
using NUnit.Framework;

namespace Chess.Test.Validations
{
    [TestFixture]
    public class KnightValidatorTests
    {
        private Mock<FileAndRankLimitValidate> _fileAndRankLimitValidateMock;

        [SetUp]
        public void Setup()
        {
            _fileAndRankLimitValidateMock = new Mock<FileAndRankLimitValidate>();

            var validator = new KnightValidator(_fileAndRankLimitValidateMock.Object);
            validator.Validate(It.IsAny<Position>());
        }

        [Test]
        public void Validate_DeveChamarFileAndRankLimitValidateSetNextValidateUmaVez()
        {
            _fileAndRankLimitValidateMock.Verify(v => v.SetNextValidate(It.IsAny<Validate>()), Times.Once);
        }

        [Test]
        public void Validate_DeveChamarFileAndRankLimitValidateIsValidUmaVez()
        {
            _fileAndRankLimitValidateMock.Verify(v => v.IsValid(It.IsAny<Position>()), Times.Once);
        }
    }
}
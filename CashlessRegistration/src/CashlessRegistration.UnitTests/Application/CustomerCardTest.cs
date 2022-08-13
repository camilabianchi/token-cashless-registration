using CashlessRegistration.Application.DTOs;
using CashlessRegistration.Application.Services;
using CashlessRegistration.Domain.DomainObjects.Exceptions;
using CashlessRegistration.Domain.Entities;
using CashlessRegistration.Domain.Repositories;
using CashlessRegistration.UnitTests.Fakers;
using FluentAssertions;
using Moq;

namespace CashlessRegistration.UnitTests.Application
{
    public class CustomerCardTest
    {
        private readonly CustomerCardService _sut;
        private readonly Mock<ICustomerCardRepository> _customerCardRepositoryMock = new();

        public CustomerCardTest()
        {
            _sut = new CustomerCardService(_customerCardRepositoryMock.Object);
        }

        [Theory(DisplayName = "SaveCard should throws domain exception when any card information is invalid")]
        [MemberData(nameof(CustomerCardRequestDTOData))]
        public async Task SaveCard_ShouldThowsDomainException_WhenCustomerIdIsInvalid(
            CustomerCardRequestDTO customerCardRequestDTO,
            string message)
        {
            // act & assert
            var error = await Assert.ThrowsAsync<DomainException>(
                async () => await _sut.SaveCard(customerCardRequestDTO));

            error.Message.Should().Be(message);
            _customerCardRepositoryMock.Verify(x => x.SaveCard(It.IsAny<CustomerCard>()), Times.Never);
        }

        [Fact(DisplayName = "SaveCard should throws service exception when save fails")]
        public async Task SaveCard_ShouldThrowsServiceException_WhenSaveFails()
        {
            // arrange
            var customerCardRequestDTO = CustomerCardFaker.ValidCustomerCardRequestDTO();

            _customerCardRepositoryMock
                .Setup(x => x.SaveCard(It.IsAny<CustomerCard>()));

            _customerCardRepositoryMock
                .Setup(x => x.UnitOfWork.Commit())
                .ReturnsAsync(() => false);

            // act & assert
            var error = await Assert.ThrowsAsync<ServiceException>(
                async () => await _sut.SaveCard(customerCardRequestDTO));

            error.Message.Should().Be("Error saving customer card information");
            _customerCardRepositoryMock.Verify(x => x.SaveCard(It.IsAny<CustomerCard>()), Times.Once);
        }

        [Fact(DisplayName = "SaveCard returns CustomerCardResponseDTO when save succeeds")]
        public async Task SaveCard_ShouldThrowsServiceException_WhenSaveSucceeds()
        {
            // arrange
            var customerCardRequestDTO = CustomerCardFaker.ValidCustomerCardRequestDTO();

            _customerCardRepositoryMock
                .Setup(x => x.SaveCard(It.IsAny<CustomerCard>()));

            _customerCardRepositoryMock
                .Setup(x => x.UnitOfWork.Commit())
                .ReturnsAsync(() => true);

            // act
            var actionResult = await _sut.SaveCard(customerCardRequestDTO);

            // assert
            actionResult.Should().BeOfType<CustomerCardResponseDTO>();
            _customerCardRepositoryMock.Verify(x => x.SaveCard(It.IsAny<CustomerCard>()), Times.Once);
        }

        [Fact(DisplayName = "ValidateToken should throws service exception when card not found")]
        public async Task ValidateToken_ShouldThrowsServiceException_WhenCardIdNotFound()
        {
            // arrange
            var customerCardValidationRequestDTO = CustomerCardFaker.ValidCustomerCardValidationRequestDTO();
            
            _customerCardRepositoryMock
                .Setup(x => x.GetByCardId(It.IsAny<long>()))
                .ReturnsAsync(() => null);

            // act & assert
            var error = await Assert.ThrowsAsync<ServiceException>(
                async () => await _sut.ValidateToken(customerCardValidationRequestDTO));

            error.Message.Should().Be("Card not found");
            _customerCardRepositoryMock.Verify(x => x.GetByCardId(It.IsAny<long>()), Times.Once);
        }

        [Fact(DisplayName = "ValidateToken should throws service exception when token is expired")]
        public async Task ValidateToken_ShouldThrowsServiceException_WhenTokenIsExpired()
        {
            // arrange
            var customerCardValidationRequestDTO = CustomerCardFaker.ValidCustomerCardValidationRequestDTO();
            var customerCardEntity = CustomerCardFaker.CustomerCardExpiredToken();

            _customerCardRepositoryMock
                .Setup(x => x.GetByCardId(customerCardValidationRequestDTO.CardId))
                .ReturnsAsync(() => customerCardEntity);

            // act & assert
            var error = await Assert.ThrowsAsync<ServiceException>(
                async () => await _sut.ValidateToken(customerCardValidationRequestDTO));

            error.Message.Should().Be("Token is expired");
            _customerCardRepositoryMock.Verify(x => x.GetByCardId(customerCardValidationRequestDTO.CardId), Times.Once);
        }

        [Fact(DisplayName = "ValidateToken should throws service exception when card does not belong to the customer")]
        public async Task ValidateToken_ShouldThrowsServiceException_WhenCardDoesNotBelogsToCustomer()
        {
            // arrange
            var customerCardValidationRequestDTO = CustomerCardFaker.ValidCustomerCardValidationRequestDTO();
            var customerCardEntity = CustomerCardFaker.CustomerCardWrongOwner();

            _customerCardRepositoryMock
                .Setup(x => x.GetByCardId(customerCardValidationRequestDTO.CardId))
                .ReturnsAsync(() => customerCardEntity);

            // act & assert
            var error = await Assert.ThrowsAsync<ServiceException>(
                async () => await _sut.ValidateToken(customerCardValidationRequestDTO));

            error.Message.Should().Be("Card number does not belong to the customer");
            _customerCardRepositoryMock.Verify(x => x.GetByCardId(customerCardValidationRequestDTO.CardId), Times.Once);
        }

        [Fact(DisplayName = "ValidateToken returns false when token is not valid")]
        public async Task ValidateToken_ReturnsFalse_WhenTokenIsNotValid()
        {
            // arrange
            var customerCardValidationRequestDTO = CustomerCardFaker.ValidCustomerCardValidationRequestDTO();
            customerCardValidationRequestDTO.Token = 2345;

            var customerCardEntity = CustomerCardFaker.CustomerCard();

            _customerCardRepositoryMock
                .Setup(x => x.GetByCardId(customerCardValidationRequestDTO.CardId))
                .ReturnsAsync(() => customerCardEntity);

            // act
            var actionResult = await _sut.ValidateToken(customerCardValidationRequestDTO);

            // assert
            actionResult.Should().BeFalse();
            _customerCardRepositoryMock.Verify(x => x.GetByCardId(customerCardValidationRequestDTO.CardId), Times.Once);
        }

        [Fact(DisplayName = "ValidateToken returns true when token is valid")]
        public async Task ValidateToken_ReturnsTrue_WhenTokenIsValid()
        {
            // arrange
            var customerCardValidationRequestDTO = CustomerCardFaker.ValidCustomerCardValidationRequestDTO();
            var customerCardEntity = CustomerCardFaker.CustomerCard();

            _customerCardRepositoryMock
                .Setup(x => x.GetByCardId(customerCardValidationRequestDTO.CardId))
                .ReturnsAsync(() => customerCardEntity);

            // act
            var actionResult = await _sut.ValidateToken(customerCardValidationRequestDTO);

            // assert
            actionResult.Should().BeTrue();
            _customerCardRepositoryMock.Verify(x => x.GetByCardId(customerCardValidationRequestDTO.CardId), Times.Once);
        }

        public static IEnumerable<object[]> CustomerCardRequestDTOData =>
            new List<object[]>
            {
                new object[] { new CustomerCardRequestDTO { CardNumber = "0", CustomerId = 0, CVV = 1}, "Customer Id is not valid" },
                new object[] { new CustomerCardRequestDTO { CardNumber = "0", CustomerId = 1, CVV = 1}, "Card Number is not valid" },
                new object[] { new CustomerCardRequestDTO { CardNumber = "4220126755596383", CustomerId = 1, CVV = 0}, "CVV is not valid" },
            };
    }
}

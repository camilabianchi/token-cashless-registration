using CashlessRegistration.Domain.DomainObjects;
using CashlessRegistration.Domain.DomainObjects.Exceptions;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashlessRegistration.Domain.Entities
{
    public class CustomerCard : 
        Entity<long>,
        IAggregateRoot
    {
        public int CustomerId { get; private set; }
        public long CardNumber { get; private set; }
        public DateTime RegistrationDate { get; private set; }

        [NotMapped]
        public long Token { get; private set; }

        [NotMapped]
        public int CVV { get; private set; }

        // EF mapping
        protected CustomerCard()
        {
        }

        public CustomerCard(int customerId, long cardNumber)
        {
            if(customerId == 0)
            {
                throw new DomainException("Customer Id is not valid");
            }

            if (!IsCardNumberValid(cardNumber.ToString()))
            {
                throw new DomainException("Card Number is not valid");
            }

            CustomerId = customerId;
            CardNumber = cardNumber;
            RegistrationDate = DateTime.UtcNow;
        }

        public long GenerateToken(int cvv)
        {
            if (cvv == 0)
            {
                throw new DomainException("CVV is not valid");
            }

            CVV = cvv;
            Token = ApplyTokenAlgorithm();

            return Token;
        }

        public bool ValidateToken(int cvv, long token)
        {
            Token = GenerateToken(cvv);

            return Token == token;
        }

        public bool IsTokenExpired()
        {
            var minutes = (DateTime.UtcNow - RegistrationDate).TotalMinutes;
            return minutes > 30;
        }

        private long ApplyTokenAlgorithm()
        {
            var lastDigits = CardNumber
                .ToString()
                .TakeLast(4)
                .ToArray()
                .Select(n => Convert.ToInt32(char.GetNumericValue(n)))
                .ToList();

            for (int i = 0; i < CVV; i++)
            {
                var last = lastDigits.LastOrDefault();
                var tempDigits = new List<int> { last };
                tempDigits.AddRange(lastDigits.Take(3).ToList());

                lastDigits = tempDigits;
            }

            var strDigits = String.Join(String.Empty, lastDigits);
            return Convert.ToInt64(strDigits);
        }

        private bool IsCardNumberValid(object? value)
        {
            if (value == null)
            {
                return true;
            }

            if (!(value is string ccValue))
            {
                return false;
            }

            ccValue = ccValue.Replace("-", string.Empty);
            ccValue = ccValue.Replace(" ", string.Empty);

            if (ccValue == "0")
            {
                return false;
            }

            var checksum = 0;
            var evenDigit = false;

            for (var i = ccValue.Length - 1; i >= 0; i--)
            {
                var digit = ccValue[i];
                if (digit < '0' || digit > '9')
                {
                    return false;
                }

                var digitValue = (digit - '0') * (evenDigit ? 2 : 1);
                evenDigit = !evenDigit;

                while (digitValue > 0)
                {
                    checksum += digitValue % 10;
                    digitValue /= 10;
                }
            }

            return (checksum % 10) == 0;
        }
    }
}

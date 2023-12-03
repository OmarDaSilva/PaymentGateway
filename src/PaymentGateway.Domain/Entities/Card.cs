using System;
namespace PaymentGateway.Domain.Entities
{
    public class Card
    {
        public CVV Cvv { get; private set; }
        public CardNumber CardNumber { get; private set; }
        public ExpiryDate ExpiryDate { get; private set; }
        public string CardHolderName { get; private set; }

        public Card(CardNumber cardNumber, CVV cvv, ExpiryDate expiryDate, string cardHolderName)
        {
            CardNumber = cardNumber;
            Cvv = cvv;
            ExpiryDate = expiryDate;
            CardHolderName = cardHolderName;
        }

        public static Card Create(CardNumber cardNumber, CVV cvv, ExpiryDate expiryDate, string cardHolderName)
        {
            return new Card(cardNumber, cvv, expiryDate, cardHolderName);
        }
    }
}


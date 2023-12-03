using System.Text.Json.Serialization;

using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Api.Requests
{
    public class BankPaymentRequest
    {
        [JsonPropertyName("card_number")]
        public string CardNumber { get; private set; }

        [JsonPropertyName("expiry_date")]
        public string ExpiryDate { get; private set; }

        [JsonPropertyName("currency")]
        public string Currency { get; private set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; private set; }

        [JsonPropertyName("cvv")]
        public string Cvv { get; private set; }

        public BankPaymentRequest(Card card, Currency currency, decimal amount)
        {
            CardNumber = card.CardNumber.Number;
            ExpiryDate = $"{card.ExpiryDate.Month}/{card.ExpiryDate.Year}";
            Currency = currency.currencyISOCode;
            Amount = amount;
            Cvv = card.Cvv.Code;
        }
    }
}

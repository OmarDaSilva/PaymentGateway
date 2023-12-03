using System.Net.Http.Json;
using Microsoft.Extensions.Options;

using PaymentGateway.Api.Requests;
using PaymentGateway.Domain.Abstractions;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Infrastructure.Settings;


namespace PaymentGateway.Infrastructure.Services
{
    public class BankService : IBankService
    {
        private readonly HttpClient _httpClient;
        private readonly string _bankServiceUrl;

        public BankService(HttpClient httpClient, IOptions<BankServiceSettings> settings)
        {
            _httpClient = httpClient;
            _bankServiceUrl = settings.Value.ServiceUrl;
        }

        public async Task<BankPaymentResult> InitiatePaymentAsync(Payment payment)
        {
            var paymentRequest = new BankPaymentRequest(payment.Card, payment.Currency, payment.Amount);

            try
            {
                var response = await _httpClient.PostAsJsonAsync(_bankServiceUrl, paymentRequest);

                if (response.IsSuccessStatusCode || response != null)
                {
                    var result = await response.Content.ReadFromJsonAsync<BankPaymentResult>();
                    if (result == null)
                    {
                        throw new Exception("Non-successful response from bank service.");
                    }
                    return result;
                }
                else
                {
                    // Handle non-successful response
                    throw new Exception("Non-successful response from bank service.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception($"Error initiating payment: {ex.Message}", ex);
            }
        }
    }
}


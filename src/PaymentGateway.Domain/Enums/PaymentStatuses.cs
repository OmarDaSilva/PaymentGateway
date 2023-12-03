namespace PaymentGateway.Domain.Enums
{
    enum PaymentStatuses
    {
        Authorized, // The payment was authorized by the call to the acquiring bank.
        Declined, // The payment was declined by the call to the acquiring bank.
        Rejected, //  No payment could be created as invalid information was supplied.
    }
}


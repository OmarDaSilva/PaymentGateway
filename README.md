# Instructions for candidates

This is the .NET version of the Payment Gateway challenge. If you haven't already read the [README.md](https://github.com/cko-recruitment) in the root of this organisation, please do so now. 

## Template structure
```
src/
    PaymentGateway.Api - a skeleton ASP.NET Core Web API
test/
    PaymentGateway.Api.Tests - an empty xUnit test project
imposters/ - contains the bank simulator configuration. Don't change this

.editorconfig - don't change this. It ensures a consistent set of rules for submissions when reformatting code
docker-compose.yml - configures the bank simulator
PaymentGateway.sln
```

Feel free to change the structure of the solution, use a different test library etc.

# Key Assumptions

### **Supported Payment Methods**

- **Assumption:** The payment gateway initially supports only credit/debit card transactions and there is no need to validate against different known card types e.g. visa and mastercard.
- **Rationale:** Focus on a high level the card payment gateway process.

### **User Authentication and Authorisation**

- **Assumption:** Merchants using the gateway are already authenticated and authorised to make payment requests.
- **Rationale:** There’s no mention of needing to handle authentication or merchant onboarding within the gateway. However, typically would use API keys for authentication (Maybe also role based access control for authorisation).

### **Compliance and Security**

- **Assumption:** Compliance with standards like PCI DSS is outside the scope of this challenge.
- **Rationale:** The challenge focuses on functionality, not compliance or security details. However, will be implementing card masking encryption for storing card number details.

### **Error Handling and Logging**

- **Assumption:** Basic error handling and logging are required but sophisticated logging mechanisms are not the focus.
- **Rationale:** The challenge emphasises simplicity and maintainability.

### **State Management**

- **Assumption:** The system needs a way to store the payments, however, each payment regardless of the outcome will only ever have one state so the state management is quite simple in this case.
- **Rationale:** The challenge specifies using a test double for storage, suggesting no integration with real databases or persistent storage.

### **Transaction Idempotency**

- **Assumption:** Ensuring idempotency of payment requests is not required for this initial implementation.
- **Rationale:** The challenge does not explicitly mention handling duplicate payment requests.

### **Scalability and Performance**

- **Assumption:** While basic performance considerations are important, extensive scalability is not a primary concern.
- **Rationale:** The challenge seems to focus more on the correctness and structure of the implementation rather than on scaling for high performance. However, the design should be able to adapt.

## ISO code considerations

- Assumption: ISO codes are strings
- **Rationale**: Since most of the system is processing using the latin alphabet I will use strings, however, some merchant systems could possibly not handle latin alphabet due to different character encoding systems etc, and as this is an API and not customer facing numeric codes would scale better.

# AC

### 1. **Process Payment**

**GIVEN** a merchant makes a payment request

**THEN** the request must include:

- card number
- expiry month/date
- amount
- currency
- cvv

**SHOULD** return an outcome of the payment in a response that must include:

- Id
- Status
- LastFourCardDigits
- Expiry  date
- Currency
- Amount (in ****Minor Unit Pricing****)

### 2. **Retrieve Payment Details**

**GIVEN** a paymentOrderId
**THEN** the merchant should be able to retrieve the details of a payment previously made.

**SHOULD** return in the response

- masked card number
- card details
- payment status code

# Design Approach Summary

Due to the nature of the task of not over engineering but also keeping in mind the increasing complexity of the payments processing world. I have decided to go for the architectural approach of combining clean architecture with CQRS.

1. **Clean Architecture**:
    - Adopted to ensure separation of concerns and layering.
    - Keeps the business logic (domain) independent of other layers where business logic doesn’t influence the design, e.g. the application and infrastructure layers
2. **Domain-Driven Design (DDD)**:
    - Focus on the business domain (e.g. models), logic, and behaviour.
    - Accurate modelling of the domain and processes within that domain.
    - Easier to test separately from other layers.
3. **CQRS (Command Query Responsibility Segregation)**:
    - Separates read and write operations into different models.
    - Fits well with Clean Architecture and DDD, especially useful in complex systems with different data access patterns for reads and writes.
    - Enhances scalability by allowing independent scaling of read and write operations.
4. **Mediator Pattern (via MediatR)**:
    - Reduces coupling between components, simplifying communication and interaction.

## Key design consideration & trade-offs

Could of went for an MVC design approach as it’s simpler, faster development, integrated approach of the user interface (api endpoints) and business logic means that it’s well suited for simple CRUD applications and cheaper. However due to the below considerations I have gone for a CQRS design.

**Pros**

- **Scalable:** Two operations read and write. The patterns are not much different in terms of what they do at a simple level, however, due to the payments being a complex world (e.g. different payment statuses, payments needing auditing logs for x changes etc..) and often needing event sourcing solutions and scalable solutions to handle xxxx+ number of payments CQRS allows for event sourcing options and if the application grows to be more complex there is the option of separating the operations into their own different services in a microservice environment allowing the option for implementing solutions to each operation.
- **Separation of concerns:** Simplifies business logic
- **Flexibility:** The dependencies can easily be swapped out for different solutions e.g. for the in memory provider I would use Monodb (For faster reads)
- **Security:** Easier to focus and apply the security needs for each operation

**Cons**

- **Complexity:** Using CQRS introduces more complexity to ensure correctness.
- **Cost:** In terms of infrastructure if scaling up and development time due to complex nature of the architecture
- **Data consistency**: The use of CQRS in this project is more organisational benefits and clear separation of concerns and the payment only has one state and business logic is simple, so for now the there isn’t concern with any operation write lag. However, when scaling the application, business logic grows, event sourcing added or even more complex transactions then this issue **must** be considered.

## System design

![cqrs.png](/images/cqrs.png)

![apiflow.png](/images/apiflow.png)

### **Payment Request/Response Fields, Validation Rules, Types:**

**Initiate Payment request:**

- **Card number:** Required, 14-19 characters, numeric. Type: string
- **Expiry month:** Required, value between 1-12. Type: string
- **Expiry year:** Required, value must be in the future. Type: string
- **Currency:** Required, 3 characters, validate against a list of ISO currency codes. Type: string
    - **Amount:** Required, integer, represents the amount in minor currency unit. type Long
- **CVV:** Required, 3-4 characters long, numeric. Type: string

**Initiate Payment response:**

- Payment Id
- Card details
    - Card number masked (string)
    - Expiry date
- Amount
- Status

**Get Payment request:**

- PaymentId

**Get Payment response:**

- Payment Id
- Card details
    - Card number masked (string)
    - Expiry date
- Amount
- Status

### ISO codes validations

- GBP (826)
- USD (840)
- EUR (978)

## Entities

Mapping for entities as these are required for communication between the application and domain layers.

Payment (Domain) → InitiatePaymentResponse (Application)

Payment (Domain) → GetPaymentResponse (Application)

Payment (Domain) → BankPaymentRequest (Domain)

Entities such as card number, cvv, expiry date are their own entities as this makes them scalable when the application grows with more business logic.

## Dependencies

BankService:

Custom HTTP client class 

- Settings file for getting bank url from appsettings enabling easier maintenance

using Microsoft.Extensions.Caching.Memory;

Acts as the test double, in real-life would use mondodb cloud. 

- **Custom Data Access Layers** (Using the Repository pattern with in-memory caching) provide more control and are suited for non-relational databases.
- Replacement: Mongodb is scalable and allows for complex data shapes

Fluent Validation:

- This checks the responses validations based on the business rules and ensures input domain layer are always in a valid state
- This works by configuring AddFluentValidationAutoValidation in the program right after the controllers. The integration of FluentValidation in this manner automates the process of validating incoming requests and ensures that data from the application layer is in a valid state before going into the domain.

MediaTr:

MediatR is used to simplify handling of **`CreatePaymentCommand`** and **`GetPaymentQuery`**, fusing the roles of CQRS commands and DTOs into a unified model. This approach not only streamlines the command-response cycle but also significantly reduces component coupling, enhancing the modularity and maintainability of our architecture.

SeriLog:

For structured logging purposes to help with debugging.

## Testing

By using the clean architecture approach testing becomes simplified by each layer having it’s own set of unit tests, below are the tests that I have considered. By having unit tests and integration tests this provides the gateway with comprehensive base to ensure that AC is met. Further tests could be added w

### Unit tests

Domain:

- Exceptions
- Encryption service
- Repository interfaces

API:

- Input Validators
- Command and Query Handlers
- Mapping Profiles

Infrastructure Layer:

- External bank service test

### Integration tests

- Initiate payment with valid/invalid inputs
- Get payment with valid/invalid inputs

Future considerations for testing:

- API contract tests
- End to End
- Performance testing

## Observability

- **Grafana Integration:**
    - Set up Grafana for visualising and monitoring application metrics such as:
        - Response times of API endpoints
        - Transactions success/failure rates
        - Resource utilisation
        - Error rates
        - Health check
- **OpenTelemetry:**
    - Implement OpenTelemetry for tracing and collecting telemetry data.

## Packaging/Hosting

- Blue green deployment strategy
- Containerisation
- Versioning each release of the docker

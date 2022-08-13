# Token generation for cashless registration

This is a .NET Core project that exposes 2 different APIs as described below:

**Register API**: this endpoint saves customer card and generates a token for it

```[POST] /api/customer-cards/save```

Payload: 
```json
{
  "customerId": 0,
  "cardNumber": "string",
  "cvv": 99999
}
```

Response: 
```json
{
  "id": 0,
  "token": 0,
  "registrationDate": "2022-08-13T14:04:02.455Z"
}
```

**Token API**: this endpoint validates a token for a given card

```[POST] /api/tokens/validate```

Payload: 
```json
{
  "customerId": 0,
  "cardId": 0,
  "token": 0,
  "cvv": 99999
}
```

Response: 
```json
true/false
```
# Assumtions and other notes

### Server side

- The only content type we support is json
- Storage is currently implemented using an in-memory store
- Validation and error handling are extremely simplistic but can be expanded on
- There will be gateways and load-balancers providing authentication mechanisms and ssl termination

- Since this service is likely to be consumed by other services, we have not introduced any user abstractions, asociations or identification. It is expected that consuming services will track user association to carts in their own way.
- Since pricing semantics can vary wildly based on different product types, pricing systems tend to become extremely complex and messy (fractional costs, discounts codes, bulk orders etc) therefore it is assumed that product pricing will be provided by external services and does not need to be accounted for here.

### Client side

- Security and authentication have not been introduced, further thought needed
- The api has been implemented as a flat api over the http protocol - object oriented or fluent abstractions can be added on top when justified
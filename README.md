# Onion DI

Layers in Onion Architecture:

+ DOMAIN MODELS LAYER - contains all core models of the application. This layer doesn't have any dependencies.
+ DOMAIN REPOSITORIES LAYER - abstration between domain layer and business logic layer. It contains repositories to retrieve data from DB
+ APPLICATION SERVICES LAYER - business logic layer. Implements all data manipulation algorithms and other calculations
+ PRESENTATION (API) LAYER - endpoint for client.

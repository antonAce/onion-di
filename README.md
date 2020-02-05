# Onion DI

Layers in Onion Architecture:

+ DOMAIN - contains all core models of the application. This layer doesn't have any dependencies.
+ DATA - abstration between domain layer and business logic layer. It contains repositories to retrieve data from DB

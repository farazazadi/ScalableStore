# Scalable Store

<img src="https://github.com/farazazadi/ScalableStore/blob/main/Images/Architecture-Diagram.png" />


Based on the assumption that the Query section of the system experiences the highest workload, the purpose of this project is to demonstrate how a scalable solution can be implemented using CQRS and Event-driven architecture.
This solution consists of three containerized microservices (`CommandSide`, `QuerySide`, `ApiGateway`) running on the Docker platform, and a web client (`Clients.WebUi`).


## How to run (The easiest way)?

1. Install and run the Docker desktop
2. Execute the `Run.bat` file in the `DemoStore` directory

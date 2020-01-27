# AR Notes

AR Notes is an augmented reality task organiser designed to improve teamwork in a shared coworking space. It features a combination of software technologies and physical interactions on a physical Kanban board to take advantage of the strengths of both the virtual and the real world. 



# Software Architecture and Technology Stack

The system is a distributed virtual environment composed of 3 different modules that communicate each other with REST calls, it’s based on a classic centralized client-server architecture.
•	ARNotes
An Android client built in Unity and powered by Vuforia AR library. It’s the main interaction point between the users and the application physical objects.
•	ARNotes – Server
A Java application that provides all the core logic and functionalities of the backend.
•	ARNotes – Web
A web interface for managing the digital version of the application objects.

To build the system we used several technologies, languages and libraries according to our requirements:
•	Unity Engine (C#) – Game engine that we use for the AR clients.
•	Vuforia – AR library we used to render the augmented tasks.
•	Java Spring (Java 13) – Last version of the OOP language we used to build the server.
•	Maven – Package manager for Java libraries and dependencies.
•	JavaScript EC6 – Last version of the web language we used to manipulate objects on the interface.
•	Bootstrap – CSS/JS library for building consistent interfaces.
•	PostgreSQL – Database management system.
•	Swagger – API viewer we used for testing the application APIs.



# Setup

You need the following softwares installed on your computer to run the project:
1. Unity 2019.2.x with Android support and Vuforia
2. Java 13 (11 is supported as well, 8 is not tested but should work)
3. Maven Package Manager
4. PostgreSQL 12.x


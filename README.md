# Real Time Chat

Backend centered ASP.NET chat application. It allows it's users to comunicate in real time. To use it you need to create an account first. As user log in, a session is made with user's connection id, and their status is changed to online. As a user you can invite other users to become your friends using their unique usernames. If they respond, friend relation will be saved in database and from now on, both friends can message each other and check theirs friends statuses.

App made using ASP.NET on .NET 6 \
All data is stored in MSSQL database, which is managed by EntityFramework Core using code-first approach.\
To communicate in real time, SignalR was implemented.\
Loggin is done using Serilog. \
Login functionality is done via Identity. \
API follows REST priciples. \
Code was written to be scalable and readable.

## Features

- Own accounts
- Adding and managing friends
- In real time communitacion
- All conversations are stored in database
- Logging to console and file

## Authors

- [@MikolajBlaszczyk](https://www.github.com/MikolajBlaszczyk)
- [@JakubJachym4](https://www.github.com/JakubJachym4)

## Database Relation Schema
![schema](https://user-images.githubusercontent.com/107769098/225423071-1e292b06-6c9e-49d2-b853-c28fc7f24690.png)

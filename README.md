# Shaiya Origin

## Notice
This repository is no longer under active development. 

## Introduction
Shaiya Origin is an emulator for the Shaiya MMORPG. The core servers are written in C#, with an extendable scripting system written in Python (not yet completed). This project was solely made for learning purposes and is not intended to be used in production.

The project is split into 4 components: the common library (contains code shared by multiple servers), the database server (accepts connections from the login and game servers, and asynchronously processes database requests, and responds with the database), the login server (connects to the database server, processes login requests, etc), and the game server (retrieves definitions and player data from the database server, and handles all game related activities). This project is designed to be fully scalable, so all these servers can be run independently. This means it's very easy to add multiple game servers using the same database/login server.

## Copyright
License: MIT
Read file: [LICENSE](LICENSE.txt)
## Images

![Login server test](https://imgur.com/a/HJPUiZQ)

![Character list test](https://imgur.com/a/LAKxlps)

![Game world test](https://imgur.com/a/r9DF3sY)




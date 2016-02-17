#Technology stack

- jQuery
- SignalR (client and server)
- ASP.NET

#Project details

The project is a client-server multiplayer game.
Multiple players join, and for each player there are 3 connections oppen with the server.
- Player connection, that is responsible for each new player
- Keypress connection - handling the keystrokes sent to server
- Game connection - pushes the current game state to the client, so we can display it
The server refreshes the game state on regular intervals and sends it to the client.
# TCP Client-Server Assignment

This project implements a basic TCP client-server architecture in C# to exchange encrypted messages based on specific business logic.

## Dependencies
.NET 8.0 


## 📄 Assignment Summary

The client sends a formatted string (e.g., `SetA-Two`) to the server.

The server:
1. Splits the input string into a key and sub-key.
2. Looks up the key in a predefined collection of string-to-value mappings.
3. If the sub-key is found, it sends the current timestamp every second for `n` seconds, where `n` is the mapped value.
4. If not found, it sends `"EMPTY"`.

---

## 🧠 Features

- **AES encryption** between client and server
- **Configurable input** (e.g., IP, port, AES key/IV)
- Server supports **multiple simultaneous clients**
- Server sends **timestamp responses** at 1-second intervals

---

## 🔧 Setup & Run

### 🧩 Clone Repositories

```bash
# Clone Server
git clone https://github.com/nimeshst/Assigment-Server.git

# Clone Client
git clone https://github.com/nimeshst/Assignment-TCPClient.git


cd Assigment-Server
dotnet build
dotnet run


cd Assignment-TCPClient
dotnet build
dotnet run

# You'll be prompted to enter input like:
SetA-Two




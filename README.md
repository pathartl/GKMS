# Game Key Management Service (GKMS)

GKMS is a utility to help distribute PC game CD keys to computers in a network. Its most useful function is to distribute valid keys to machines in environments like LAN parties or multi-gamer households.

## What it is not

GKMS is **NOT** a key generator. No license keys are shipped with GKMS, *you must provide your own*.

## Why does this exist?

A group of friends and I like to gather to play 90's-00's era games in classic LAN party fashion. One of the most frustrating things is making sure everyone gets a valid CD key. This utility was created to help change the CD key on each machine while a server hands out keys from a list. Spreadsheets are tedious and have to be maintained, so why not have a system to manage licensing for you?

## How does it work?

A machine must be set up as the server. That is, a machine must be dedicated to running the main server executable (`GKMS.exe`) at all times. The server will read game manifest files (`.json` files in the `Games` directory at the same level as `GKMS.exe`) and will listen for clients that need a new key.

Separately, each client on the network that wants to change their key should run the `GKMS.Client.exe` executable. Using the interface, players can click on their desired game to see their current CD key. Clicking on the "Get Key" button will send a request out to the server and will change the CD key once the server responds.

## Infrastructure

GKMS doesn't do anything fancy and implements a very basic protocol. UDP packets are sent/received on port 420. The GKMS.Common class library contains all definitions of supported games and packet types. Standard packets look something like this:

```<PacketType><MACAddress><Message>```

Some notes:

* Packet types are used to designate what kind of packet is being send. The most used are ServerAllocateKey and ClientChangeKey. These are implemented in C# using enums.
* Yes, it uses UDP. No real reason. If you want TCP, feel free to submit a pull request.
* Port is currently not changeable.
* ServerAllocateKey packets broadcast a packet that looks like `06000000000000Battlefield1942` where the first byte `06` is the int value of the ServerAllocateKey PacketType enum value, the next six bytes `000000000000` is the client's MAC address, and `Battlefield1942` is the class name of the supported game.
* Packets requesting keys from the server are sent via the network's broadcast address. The server will respond directly to the client.

## Why does GKMS.Client require admin privileges?

Windows requires admin privileges to modify the registry. Since most games store their CD key within the registry, we need to be able to write here.

## Where are the CD keys stored?

Most CD keys for games in the late 90's to early 00's store their key in the Windows registry. In 64-bit modern versions of Windows, these keys are usually stored under the root of `HKEY_LOCAL_MACHINE\Software\WOW6432Node`. GKMS is smart enough to know where in the registry to put the key.

## How do I configure my GKMS server?

1. Start GKMS.exe on your server
2. Start GKMS.Client.exe on a client machine on the same network
3. Use the client to request a key for the game you want to configure on the server
4. On the server a `Games` folder will have been created and a `.json` file for the game will be created inside
5. Load your keys into the game's `.json` file in the `Keys` array. Most likely your game will not require hyphens.

A proper `.json` file will look something like this:

```
{
	"Name": "Battlefield 1942",
	"Keys": [
		"2422798080464862971729",
		"3689756084024224560336",
		"1021530887678678639231",
		"9811028908169258253354"
	]
}
```

The `Name` field isn't all that important and really is only used to remind you which config you're editing.

**DO NOT RENAME THE JSON FILE! THE SERVER WILL NOT BE ABLE TO READ IT IF YOU RENAME IT**

## The list of games you have isn't everything, how do I add a new game?

This is currently not implemented. If you know Git/Github take a look at the definitions within the GKMS.Common project and feel free to submit a pull request.

## How do I check which keys have been allocated?

It's a bit manual at the moment. `GKMS.db` is a SQLite database, so you can browse that data if you want to know.

## These binaries are huge! Do you really need <insert comment about .NET bloat here>?

Yes. Got a problem? Contribute!

But for real, the project is built in .NET 5.0. The server binary could run on Linux if you wanted to compile it.

## Known issues

* Keys are pulled from the `.json` files randomly
* Port is currently not configurable
* There's no command line arguments. It'd be nice to have these to automate key changes.
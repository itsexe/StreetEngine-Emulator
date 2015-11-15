# StreetEngine-Emulator
[![StreetEngine Chat](https://img.shields.io/badge/StreetEngine-JOIN%20CHAT%20%E2%86%92-brightgreen.svg?style=flat-square)](https://gitter.im/greatmaes/StreetEngine-Emulator?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=body_badge) ![Version](https://img.shields.io/badge/Version-1.0.0.1-brightgreen.svg?style=flat-square) ![Platform](https://img.shields.io/badge/Platform-any%20windows-brightgreen.svg?style=flat-square)

*Sorry for bad english grammar, this is not my native language, I did my best.*

StreetEngine is a non-profit server side emulator for the game StreetGears. This is mostly a project to learn how to make your own emulator as I don't have the knowledge to fully finish this project. I'd like to release one update atleast every three weeks. Feel free to contribute.

Note: This is my first time with packet.

- [**1. Setup**](#database-setup)
  - [1.1 MySQL Database installation](#database-setup)
  - [1.2 StreetEngine setup](#streetengine-setup)
  - [1.3 Comptability](#comptability-setup)
- [**2. Informations**](#database-informations)
  - [2.1 Database](#database-informations)
  - [2.2 Packets](#packets-informations)
- [**3. Community**](https://github.com/greatmaes/StreetEngine-Emulator/wiki)
  - [3.1 Isssues tracker](https://github.com/greatmaes/StreetEngine-Emulator/issues)
  - [3.2 Wiki](https://github.com/greatmaes/StreetEngine-Emulator/wiki)

### Database (*Setup*)

First you will need to create a MySQL Database. I use [this website](http://www.freemysqlhosting.net/) for a quick and free database to test my stuff, you can also use a local database. Then just restore my sql file to your server using Navicat (or any database manager) like this.

![Image1](https://raw.githubusercontent.com/greatmaes/StreetEngine-Emulator/master/src/EngineAssets/Screenshots/executesql-1.jpg)
![Image2](https://raw.githubusercontent.com/greatmaes/StreetEngine-Emulator/master/src/EngineAssets/Screenshots/executesql-2.jpg)

Passwords must be MD5 hashed (See changelog: 1.0.0.1). You can use any web based encrypter to do this.
I use "*MD5Encryption.com*" in this example. (Bad website because you can decrypt hash as they save what you encrypt in a database)

![Image3](https://raw.githubusercontent.com/greatmaes/StreetEngine-Emulator/master/src/EngineAssets/Screenshots/md5-encryption.jpg)

Now simply copy your MD5 hashed password and paste it in your database. (Must be upper case)

![Image4](https://raw.githubusercontent.com/greatmaes/StreetEngine-Emulator/master/src/EngineAssets/Screenshots/md5-encryption-2.jpg)

Once it's done configure the database part of the config.ini file with your informations like this.

```
[Database]
Host=YOUR_DB_HOST
User=YOUR_DB_USERNAME
Password=YOUR_DB_PASSWORD
Database=YOUR_DB_NAME
```

### StreetEngine (*Setup*)

Place every files you downloaded in your StreetGears folder.

![Image5](https://raw.githubusercontent.com/greatmaes/StreetEngine-Emulator/master/src/EngineAssets/Screenshots/Screenshot-4.jpg)

Finally, start "*StreetEngine.exe*" and inject "*KiLLer.dll*" using Winject or any DLL injector to disable packets encryption.

### Comptability (*Setup*)

If you have any problem running both game and emulator on windows 7/vista/xp then try to change comptability settings to "*Windows 95*" of both game and emulator like this, this may help.

![Image6](https://raw.githubusercontent.com/greatmaes/StreetEngine-Emulator/master/src/EngineAssets/Screenshots/comptability.jpg)

### Database (*Informations*)
Key | Description
--- | -----------
id    | (int) Player's id
user   | (string) Player's username to log
password    | (string) Player's password
email   | (string) Player's email
first_login   | (int) That's also a bool. Used to check if the player logged before.
auth_key    | (string) Unique player session key.
char_rank   | (string) Admin,GameMaster,Player,Bot,Banned.
char_type   | (int) Player's character. 0=Luna, 1=Tippy, 2=Rush, 3=Rookie, 4=Kara, 5=Klaus.
char_level  | (int) Player's level. 0-45
char_exp    | (int) Player's experience. 0-102400 (0%-100%)
char_liscence   | (int) Player's liscence. 0-4
char_tricks   | (int) Player's trick level list. Actual order: Grind,Backflip,FrontFlip,Airtwist,Powerswing,Gripturn,Dash,Backskating,Jumpingsteer,Butting,Powerslide,Powerjump,Wallride
char_gpotatos   | (int) Player's gpotatos. 0-999,999,999
char_rupees   | (int) Player's rupees. 0-999,999,999
char_coins   | (int) Player's coins. 0-999,999,999
char_questpoints   | (int) Player's questpoints. 0-999,999,999
char_clanid   | (string) Player's clan id. 4-Lenght Long. Example: "CL#1". 
char_clanname   | (string) Player's clan name.
char_age    | (int) Player's age.
char_bio    | (string) Player's bio.
char_zoneid   | (int) Player's country.
char_zoneinfo   | (string) Player's country info.

### Packets (*Informations*)
Lenght | Header | Hash
------ | ------ | ----
First 2 bytes of the packet. | Next 2 bytes after the lenght. | 5th byte of the packet (the last one).

# Binaries
- [**Lastest update (v1.0.0.1)**](https://github.com/greatmaes/StreetEngine-Emulator/releases/tag/1.0.0.1)
  - [Binary](https://github.com/greatmaes/StreetEngine-Emulator/releases/download/1.0.0.1/StreetEngine-Emulator-Binary.rar)
  - [Full Source Code](https://github.com/greatmaes/StreetEngine-Emulator/releases/download/1.0.0.1/StreetEngine-Emulator-Full-Source.rar)
- [**Update v1.0.0.0**](https://github.com/greatmaes/StreetEngine-Emulator/releases/tag/1.0.0.0)
  - [Binary](https://github.com/greatmaes/StreetEngine-Emulator/releases/download/1.0.0.0/StreetEngine-Emulator-Binary.rar)
  - [Full Source Code](https://github.com/greatmaes/StreetEngine-Emulator/releases/download/1.0.0.0/StreetEngine-Emulator-Full-Source.rar)

# Credits
- http://github.com/itsexe/ teached me about StreetGear's packets and helping me with login.
- http://github.com/skeezr/ helped me with TCP sockets and SilverSock.
- http://www.elitepvpers.com/forum/members/4193997-k1ramox.html for packets encryption offsets. (*Killer dll*)


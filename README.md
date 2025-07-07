📦 Seq.Telegram
A plugin for Seq 4.2 designed to send notifications to Telegram when events of a certain level occur.

🔧 Features
Sending messages to a Telegram chat or channel

Simple configuration via the Seq interface

It works with the Seq 4 version.2

🚀 Installation
Download and install the package via NuGet or as a Seq plugin.

In the Seq settings, add a new signal with the necessary filters.

Specify:

Telegram bot token

Chat ID (where to send notifications)

📌 Requirements
Seq version 4.2
is a registered Telegram Bot

## Packages :
* **Seq.App.URL** for sending log status via `POST` request
* **Seq.AppT.elegram** for sending status logs to a telegram group via a `Bot`

## Commands: 
* **GitHub Package push command** `dotnet nuget push "bin/Release/PROJECT_NAME.1.0.0.nupkg"  --api-key YOUR_GITHUB_PAT --source "github" `

* **Restore command** 

`
seq show-key

seq stop

seq restore -k="secret-key" -b="-backup-file-name.seqbac"

seq start`

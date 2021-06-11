# Dilly
[![](https://img.shields.io/badge/net%20core-5-blue?logo=visual-studio)](https://dotnet.microsoft.com/download) [![](https://img.shields.io/badge/docker-blue?logo=docker)](https://dotnet.microsoft.com/download)

Educational Project showing how Kafka can be used as a publish-subscribe based messaging system.

### Requirements

1. net 5
2. Docker

### Windows setup

1. Run `Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass` to allow PowerShell scripts.
2. Open PowerShell/VS Code Terminal and run `curl https://raw.githubusercontent.com/akovanev/Dilly/main/.local-setup/scripts/Setup.ps1 -o  Setup.ps1;  ./Setup.ps1`

### Troubleshooting

It may happen that `Dilly.Service` and/or subscribers (DS&S) will not be working at the first run. CD to the `.local-setup/scripts` folder, close all DS&S windows and run `./RunDilly.ps1` 

### Components

![image](https://user-images.githubusercontent.com/3360126/121689437-e2a81880-cac4-11eb-9b81-91cf208bb79b.png)

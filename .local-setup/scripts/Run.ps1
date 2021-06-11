# If PowerShell blocks the script - run next line first.
# Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass

& .\RunThirdPartyContainers.ps1

& .\RunPublishers.ps1

& .\RunDilly.ps1
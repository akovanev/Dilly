  
# This script will help you download, build and run Billy!
#
# Make sure that you have git and .net core 3.1 installed and enjoy!
#
# If PowerShell blocks the script - run next line first.
# Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass
#
# Let Billy get started!

# Comment next line if you already have Dilly.
git clone https://github.com/akovanev/Dilly.git

Set-Location $pwd/Dilly/.local-setup/scripts

& .\Run.ps1
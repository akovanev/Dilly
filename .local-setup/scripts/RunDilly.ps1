function StartService {
    param (
        $Folder
    )
    Set-Location $Folder
    Start-Process "cmd" -ArgumentList "/c dotnet build && dotnet run"
}

$path=$pwd
try {

    #CD to root
    Set-Location ../..

    #Dilly.Service
    $DillyFolder = "$pwd/Dilly/src/Dilly.Service"
    #Dilly Imdb
    $ImdbFolder = "$pwd//Dilly/src/Dilly.Integrations.Imdb"
    #Dilly LetterBoxd
    $LetterBoxdFolder = "$pwd//Dilly/src/Dilly.Integrations.LetterBoxd"

    StartService -Folder $DillyFolder
    StartService -Folder $ImdbFolder
    StartService -Folder $LetterBoxdFolder
}
finally {
    Set-Location $path  
}
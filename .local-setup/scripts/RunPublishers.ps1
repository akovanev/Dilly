function StartPublisher {
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

    #Disney
    $DisneyFolder = "$pwd/FilmMakers/Disney"
    #Netflix
    $NetflixFolder = "$pwd/FilmMakers/Netflix"

    StartPublisher -Folder $DisneyFolder
    StartPublisher -Folder $NetflixFolder
}
finally {
    Set-Location $path  
}
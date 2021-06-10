function StopContainers {
	docker stop $(docker ps -a -q)
}

function RunKafka {
	param ($Folder)
	Set-Location $Folder
	docker-compose up -d
}

function ShowContainers {
	docker ps
}

$path=$pwd
try {
	Set-Location ..
	StopContainers
	RunKafka -Folder "$pwd/docker/kafka"
	ShowContainers 
}
finally {
    Set-Location $path  
}
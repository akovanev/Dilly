function StopContainers {
	docker stop $(docker ps -a -q)
	docker system prune -f
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
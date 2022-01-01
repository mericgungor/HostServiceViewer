$mypath = $MyInvocation.MyCommand.Path
$mypath1 = Split-Path $mypath -Parent
Push-Location $mypath1

$versiyon="22.01.01.1830"

docker build -t mericgungor/host-service-viewer-mongodb:$versiyon .
docker image push mericgungor/host-service-viewer-mongodb:$versiyon

#docker build -t mongodb .
#docker commit mongodb mericgungor/host-service-viewer-mongodb:$versiyon
#docker image push mericgungor/host-service-viewer-mongodb:22.01.01.1750


# docker container run -d --name mongodb -p 27017:27017 -v mongodb-volume:/data/db mericgungor/host-service-viewer-mongodb:22.01.01.1830
pause
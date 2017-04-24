docker build -t build-image -f Dockerfile.build
docker create --name build-container build-image
docker cp build-container:/out ./dist
docker rm build-container
docker rmi build-image
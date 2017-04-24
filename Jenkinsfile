node('swarm') {

	checkout scm
	sh "git rev-parse HEAD > .git/commit-id"
	def commit_id = readFile('.git/commit-id').trim()
	println commit_id
    
	stage "build artifacts"
		sh '''
		docker build -t build-image -f Dockerfile.build .
		docker create --name build-container build-image
		docker cp build-container:/out ./dist
		docker rm build-container
		docker rmi build-image
		'''

	stage "build optimized image"
		def app = docker.build 'appsflare/pro-ideas-v2:latest'
		app.push()    
}
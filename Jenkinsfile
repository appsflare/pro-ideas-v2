node('swarm') {

	checkout scm
	sh "git rev-parse HEAD > .git/commit-id"
	def commit_id = readFile('.git/commit-id').trim()
	println commit_id
    
	stage "build artifacts"

		def sdk = docker.image('microsoft/dotnet:1.1-sdk')
		sdk.pull()
		sdk.inside{
			sh '''
				cd src/ProIdeas.UI
				dotnet restore
				dotnet publish -o ./dist
			'''
		}		
		// sh '''
		// docker build -t build-image -f Dockerfile.build .
		// docker create --name build-container build-image
		// docker cp build-container:/out ./dist
		// docker rm build-container
		// docker rmi build-image
		// '''

	stage "build image"
	docker.withRegistry('https://registry.hub.docker.com/v2/', 'docker-hub') {    
		def app = docker.build 'appsflare/pro-ideas-v2:latest'
		app.push()    
	}
}
node('swarm') {

	checkout scm
	sh "git rev-parse HEAD > .git/commit-id"
	def commit_id = readFile('.git/commit-id').trim()
	println commit_id
    
	stage "build artifacts"
		println 'building app'
		sh 'docker build -t build-image -f Dockerfile.build'
		println 'building app'
		sh 'docker create --namet build-container build-image'
		sh 'docker cp build-container:/out ./dist'
		sh 'docker rmi -image'
		sh 'docker rm build-container'

	stage "build optimized image"
		def app = docker.build 'appsflare/pro-ideas-v2:latest'
		app.push()    
}
node('swarm') {

	checkout scm
	sh "git rev-parse HEAD > .git/commit-id"
	def commit_id = readFile('.git/commit-id').trim()
	println commit_id
    
	stage "build artifacts"
		sh './build-artifacts.sh'

	stage "build optimized image"
		def app = docker.build 'appsflare/pro-ideas-v2:latest'
		app.push()    
}
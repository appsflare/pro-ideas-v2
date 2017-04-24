node('swarm') {

	checkout scm
	sh "git rev-parse HEAD > .git/commit-id"
	def commit_id = readFile('.git/commit-id').trim()
	println commit_id
    
	stage "build artifacts"
		println 'building app'
		sh 'docker build -t ${commit_id}-image -f Dockerfile.build'
		println 'building app'
		sh 'docker create --namet ${commit_id}-container ${commit_id}-image'
		sh 'docker cp ${commit_id}-cont:/out ./dist'
		sh 'docker rmi ${commit_id}-image'
		sh 'docker rm ${commit_id}-container'

	stage "build optimized image"
		def app = docker.build 'appsflare/pro-ideas-v2:latest'
		app.push()    
}
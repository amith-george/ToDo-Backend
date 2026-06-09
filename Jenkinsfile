pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Deploy Backend') {
            steps {
                // Ensures the external network exists before compose tries to attach to it
                bat "docker network create todo-net 2>nul || exit 0"
                
                // Spin up both database and API using Compose with a unified project name
                bat "docker compose -p todoapp up -d --build"
            }
        }
    }
}

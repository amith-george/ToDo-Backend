pipeline {
 
    agent any
 
    environment {
        IMAGE = "todo-api:${BUILD_NUMBER}"
        NETWORK = "todo-net"
        MYSQL_CONT = "todo-mysql"
        API_CONT = "todo-api"
 
        MYSQL_PWD = "root"
        MYSQL_DB = "tododb"
    }
 
    stages {
 
        stage('Checkout') {
            steps {
                checkout scm
            }
        }
 
        stage('Build Docker Image') {
            steps {
                bat "docker build -t %IMAGE% ."
            }
        }
 
        stage('Create Network') {
            steps {
                // Ignore errors if network already exists
                bat "docker network create %NETWORK% 2>nul || exit 0"
            }
        }
 
        stage('Start MySQL') {
            steps {
                bat """
                docker rm -f %MYSQL_CONT% 2>nul
 
                docker run -d --name %MYSQL_CONT% --network %NETWORK% ^
                    -e MYSQL_ROOT_PASSWORD=%MYSQL_PWD% ^
                    -e MYSQL_DATABASE=%MYSQL_DB% ^
                    -p 3306:3306 ^
                    -v todo-mysql-data:/var/lib/mysql ^
                    mysql:8.0
                """
            }
        }
 
        stage('Wait for MySQL (HEALTHCHECK equivalent)') {
            steps {
                bat """
                echo Waiting for MySQL to be ready...
 
                :loop
                docker exec %MYSQL_CONT% mysqladmin ping -h localhost -uroot -p%MYSQL_PWD% >nul 2>&1
 
                IF ERRORLEVEL 1 (
                    timeout /t 5 >nul
                    goto loop
                )
 
                echo MySQL is ready!
                """
            }
        }
 
        stage('Run API') {
            steps {
                bat """
                docker rm -f %API_CONT% 2>nul
 
                docker run -d --name %API_CONT% --network %NETWORK% ^
                    -e ASPNETCORE_ENVIRONMENT=Development ^
                    -e ASPNETCORE_URLS=http://+:5024 ^
                    -e ConnectionStrings__DefaultConnection="Server=%MYSQL_CONT%;Database=%MYSQL_DB%;User=root;Password=%MYSQL_PWD%;" ^
                    -e JwtSettings__Issuer=ToDoApi ^
                    -e JwtSettings__Audience=ToDoApiClient ^
                    -e JwtSettings__SecretKey=SuperSecretKeyForToDoApiThatShouldBeLongEnoughToWork!1234567890 ^
                    -p 5024:5024 ^
                    %IMAGE%
                """
            }
        }
 
    }
}

#!/bin/bash
docker volume create chat-app-data
docker build -t chat-app .
docker run -v chat-app-data:/code -p 5000:5000 chat-app

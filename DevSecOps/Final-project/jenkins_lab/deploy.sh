#!/bin/bash

# Docker image details
IMAGE_NAME="my-jenkins-image"
IMAGE_TAG="latest"
VM_INSTANCE="miriam-jenkins"
# GCP Artifact Registry details
GCP_PROJECT_ID="devconnect-project"
GCP_REGION="me-west1"
ARTIFACT_REGISTRY="miriam-artifacts"

# Authenticate with GCP using the gcloud CLI
gcloud config configurations activate final-project
gcloud auth login --no-launch-browser
gcloud config set project $GCP_PROJECT_ID
gcloud auth configure-docker me-west1-docker.pkg.dev
# Configure the gcloud CLI for your project
gcloud config set project "$GCP_PROJECT_ID"

# Configure the gcloud CLI for the Artifact Registry repository
gcloud artifacts repositories configure-docker "$ARTIFACT_REGISTRY" \
  --location="$GCP_REGION"

# Build the Docker image
docker build -t "me-west1-docker.pkg.dev/$GCP_PROJECT_ID/$ARTIFACT_REGISTRY/$IMAGE_NAME:$IMAGE_TAG" .

# Authenticate with GCP using the docker credential helper
gcloud auth print-access-token | docker login -u oauth2accesstoken --password-stdin "https://me-west1-docker.pkg.dev"

docker tag $IMAGE_NAME $GCP_REGION-docker.pkg.dev/$GCP_PROJECT_ID/$ARTIFACT_REGISTRY/$IMAGE_NAME:$IMAGE_TAG

# Push the Docker image to the GCP Artifact Registry repository
docker push "me-west1-docker.pkg.dev/$GCP_PROJECT_ID/$ARTIFACT_REGISTRY/$IMAGE_NAME:$IMAGE_TAG"
gcloud compute ssh "$VM_INSTANCE" --zone="me-west1-a" --command="docker run -d -p 8080:8080 me-west1-docker.pkg.dev/$GCP_PROJECT_ID/$ARTIFACT_REGISTRY/$IMAGE_NAME:$IMAGE_TAG"
# Clean up: Remove the local image (optional)
docker rmi "me-west1-docker.pkg.dev/$GCP_PROJECT_ID/$ARTIFACT_REGISTRY/$IMAGE_NAME:$IMAGE_TAG"

# Log out (optional)
docker logout

# Revoke the gcloud CLI authorization (optional)
gcloud auth revoke

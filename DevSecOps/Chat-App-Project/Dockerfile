# set base image (host OS)
FROM python:3.8
RUN update-ca-certificates
# ENV ROOMS_DIR='rooms'
# set the working directory in the container
WORKDIR /code
# copy the dependencies file to the working directory
RUN pip install Flask
COPY requirements.txt requirements.txt
RUN pip install -r requirements.txt
ENV FLASK_ENV development
ENV ROOMS_DIR='ROOMS'
# install dependencies
# RUN pip install --trusted-host pypi.org --trusted-host files.pythonhosted.org -r requirements.txt
# copy the content of the local src directory to the working directory
COPY . .
# command to run on container start
CMD [ "python", "./chatApp.py" ]

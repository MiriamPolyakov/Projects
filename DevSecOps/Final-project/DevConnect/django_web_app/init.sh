docker volume create my_django_volume
docker build -t my-django-app .
docker run -p 8000:8000 -v my_django_volume:/django_web_app my-django-app 

# Use the latest Python image
FROM python:latest

# Set environment variables for Django
ENV DJANGO_SETTINGS_MODULE=django_web_app.settings
ENV DJANGO_SECRET_KEY=your_secret_key


# Set the working directory in the container
WORKDIR /django_web_app

# Copy the requirements.txt file and install dependencies
COPY requirements.txt /django_web_app/
RUN pip install -r requirements.txt
COPY . .


# Expose the port your Django app will run on
EXPOSE 8000

# Run the Django development server
CMD ["python", "manage.py", "runserver", "0.0.0.0:8000"]

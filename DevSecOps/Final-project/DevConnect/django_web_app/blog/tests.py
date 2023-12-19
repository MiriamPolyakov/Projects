from django.test import TestCase

# Create your tests here.
class MyTest(TestCase):
    def test_app_response(self):
        response = self.client.get("/")
        self.assertEqual(response.status_code, 200)
1) Replace Twilio credentials with real values from your Twilio dashboard
2) Run Redis:
docker run --name redis -d -p 6379:6379 redis

3) Open Swagger UI at:
http://localhost:5000/swagger

4) Test the POST API endpoint /api/sms/send by sending:
{
  "PhoneNumber": "+1234567890",
  "Message": "Hello from Twilio!"
}
5) Verify Responses:
   Success: { "Message": "SMS sent successfully", "MessageSid": "SMXXXXXXXXXXX" }
   Rate Limit Exceeded: { "Message": "Rate limit exceeded. Try again later." }

<img width="938" alt="1" src="https://github.com/user-attachments/assets/e2fe56f4-3380-4d09-96e2-6b7a4140c3ca" />

<img width="937" alt="2" src="https://github.com/user-attachments/assets/0712805b-75cf-4f52-8dc4-f45d8d1a4753" />

<img width="947" alt="3" src="https://github.com/user-attachments/assets/627b267a-38ee-463a-b82d-e5648699b66f" />



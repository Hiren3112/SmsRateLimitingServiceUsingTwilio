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

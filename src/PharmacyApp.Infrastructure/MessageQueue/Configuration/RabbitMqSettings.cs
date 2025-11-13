using System;



namespace PharmacyApp.Infrastructure.MessageQueue.Configuration{
    //messagebussettings
 public class RabbitMqSettings
    {
        public string Host { get; set; } = "localhost";
        public string VirtualHost { get; set; } = "/";
        public string Username { get; set; } = "aaaa";
        public string Password { get; set; } = "aaaa";
        public int Port { get; set; } = 5672;
    }
}
namespace Ottawa.Pigeon.Infrastructure
{
    /// <summary>
    /// Classe qui définit les settings de connexion au Smtp Sever
    /// </summary>
    public class EmailConfiguration
    {
        public string From { get; set; } = string.Empty;
        public string SmtpServer { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

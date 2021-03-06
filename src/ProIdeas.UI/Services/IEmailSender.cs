﻿using System.Threading.Tasks;

namespace ProIdeas.UI.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}

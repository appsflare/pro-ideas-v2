﻿using System.Threading.Tasks;

namespace ProIdeas.UI.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}

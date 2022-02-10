﻿using System.Threading.Tasks;

namespace NavAppDemo.Activation
{
    public interface IActivationHandler
    {
        bool CanHandle(object args);

        Task HandleAsync(object args);
    }
}

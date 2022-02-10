using System;

namespace NavAppDemo.Contracts.Services
{
    public interface IPageService
    {
        Type GetPageType(string key);
    }
}

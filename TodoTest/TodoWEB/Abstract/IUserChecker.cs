using TodoWEB.Models;

namespace TodoWEB.Abstract
{
    public interface IUserChecker
    {
        WebUser IsValid(string login, string password);
    }
}

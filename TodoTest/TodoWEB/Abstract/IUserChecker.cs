using System.Threading.Tasks;

namespace TodoWEB.Abstract
{
    public interface IUserChecker
    {
        bool IsValid(string login, string password);
    }
}

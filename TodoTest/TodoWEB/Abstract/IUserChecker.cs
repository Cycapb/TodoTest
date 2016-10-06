namespace TodoWEB.Abstract
{
    public interface IUserChecker
    {
        bool IsValid(string userName, string password);
    }
}

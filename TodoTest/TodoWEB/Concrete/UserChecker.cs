using System.Linq;
using TodoDAL.Abstract;
using TodoDAL.Models;
using TodoWEB.Abstract;
using TodoWEB.Models;

namespace TodoWEB.Concrete
{
    public class UserChecker:IUserChecker
    {
        private readonly IRepository<User> _userRepository;

        public UserChecker(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public WebUser IsValid(string login, string password)
        {
            var user =
                _userRepository.GetList().FirstOrDefault(x => x.Login == login && x.Password == password);
            if (user == null)
            {
                return null;
            }
            return new WebUser()
            {
                Login = user.Login,
                Password = user.Password,
                UserId = user.UserId
            };
        }
    }
}
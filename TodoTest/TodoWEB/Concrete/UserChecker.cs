using System.Linq;
using TodoDAL.Abstract;
using TodoDAL.Models;
using TodoWEB.Abstract;

namespace TodoWEB.Concrete
{
    public class UserChecker:IUserChecker
    {
        private readonly IRepository<User> _userRepository;

        public UserChecker(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public bool IsValid(string login, string password)
        {
            var user =
                _userRepository.GetList().FirstOrDefault(x => x.Login == login && x.Password == password);
            return user != null;
        }
    }
}
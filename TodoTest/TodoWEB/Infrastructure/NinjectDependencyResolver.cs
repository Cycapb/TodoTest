using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Paginator.Abstract;
using Paginator.Concrete;
using TodoDAL.Abstract;
using TodoDAL.Concrete;
using TodoDAL.Models;
using TodoWEB.Abstract;
using TodoWEB.Concrete;

namespace TodoWEB.Infrastructure
{
    public class NinjectDependencyResolver:IDependencyResolver
    {
        private readonly IKernel _kernel;
        public IKernel Kernel { get { return _kernel; } }
        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }

        private void AddBindings()
        {
            _kernel.Bind<IUserChecker>().To<UserChecker>();
            _kernel.Bind<ITodoManager>().To<TodoManager>();
            _kernel.Bind<IRepository<User>>().To<EntityRepository<User>>();
            _kernel.Bind<IRepository<Todo>>().To<EntityRepository<Todo>>();
            _kernel.Bind<IPaginator>().To<Paginator.Concrete.Paginator>();
            _kernel.Bind<IPageCreator>().To<AjaxPageCreator>();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
    }
}
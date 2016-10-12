using System;
using System.Web.Mvc;

namespace Paginator.Abstract
{
    public interface IPageCreator
    {
        MvcHtmlString CreatePages(int itemsPerPage, int currentPage, int totalItems, Func<int, string> pageUrl);
    }
}
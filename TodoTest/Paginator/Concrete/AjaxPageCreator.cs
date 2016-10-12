using System;
using System.Text;
using System.Web.Mvc;
using Paginator.Abstract;

namespace Paginator.Concrete
{
    public class AjaxPageCreator:IPageCreator
    {
        private readonly IPaginator _paginator;

        public AjaxPageCreator(IPaginator paginator)
        {
            _paginator = paginator;
        }

        public MvcHtmlString CreatePages(int itemsPerPage, int currentPage, int totalItems, Func<int, string> pageUrl)
        {
            var totalPages = (int)Math.Ceiling((decimal)totalItems / itemsPerPage);
            StringBuilder result = new StringBuilder();
            if (currentPage > 1)
            {
                result.Append(CreateArrow(false, currentPage, pageUrl));
            }

            var pageItems = _paginator.Paginate(currentPage, totalPages);

            for (int i = 1; i <= pageItems.Count; i++)
            {
                if (pageItems[i - 1].Page == "...")
                {
                    var dotTag = new TagBuilder("button");
                    dotTag.InnerHtml = "...";
                    dotTag.AddCssClass("btn btn-default");
                    result.Append(dotTag);
                }
                else
                {
                    TagBuilder tag = new TagBuilder("a");
                    tag.MergeAttribute("href", pageUrl(int.Parse(pageItems[i - 1].Page)));
                    tag.InnerHtml = pageItems[i - 1].Page;
                    if (pageItems[i - 1].Page == currentPage.ToString())
                    {
                        tag.AddCssClass("selected");
                        tag.AddCssClass("btn-primary");
                    }
                    tag.AddCssClass("btn btn-default");
                    tag.MergeAttribute("data-ajax", "true");
                    tag.MergeAttribute("data-ajax-mode", "replace");
                    tag.MergeAttribute("data-ajax-update", "#todoPanel");
                    tag.MergeAttribute("data-ajax-url", pageUrl(int.Parse(pageItems[i - 1].Page)));
                    result.Append(tag);
                }
            }

            if (currentPage < totalPages)
            {
                result.Append(CreateArrow(true, currentPage, pageUrl));
            }

            return MvcHtmlString.Create(result.ToString());
        }

        private static string CreateArrow(bool forward, int currentPage, Func<int, string> pageUrl)
        {
            TagBuilder nextTag = new TagBuilder("a");
            nextTag.MergeAttribute("href", pageUrl(forward ? currentPage++ : currentPage--));
            nextTag.InnerHtml = forward ? ">>" : "<<";
            nextTag.AddCssClass("btn btn-default");
            nextTag.MergeAttribute("data-ajax", "true");
            nextTag.MergeAttribute("data-ajax-mode", "replace");
            nextTag.MergeAttribute("data-ajax-update", "#todoPanel");
            nextTag.MergeAttribute("data-ajax-url", pageUrl(forward ? currentPage++ : currentPage--));
            return nextTag.ToString();
        }
    }
}

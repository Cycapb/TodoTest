using System;
using System.Text;
using System.Web.Mvc;
using TodoWEB.Models;

namespace TodoWEB.Helpers
{
    public static class PagingHelper
    {
        public static MvcHtmlString CreatePages(this AjaxHelper htmlHelper, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            if (pagingInfo.CurrentPage > 1)
            {
                result.Append(CreateArrow(false, pagingInfo.CurrentPage, pageUrl));
            }
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                tag.MergeAttribute("data-ajax","true");
                tag.MergeAttribute("data-ajax-mode","replace");
                tag.MergeAttribute("data-ajax-update","#todoPanel");
                tag.MergeAttribute("data-ajax-url",pageUrl(i));
                result.Append(tag.ToString());
            }
            if (pagingInfo.CurrentPage < pagingInfo.TotalPages)
            {
                result.Append(CreateArrow(true, pagingInfo.CurrentPage, pageUrl));
            }

            return MvcHtmlString.Create(result.ToString());
        }

        private static string CreateArrow(bool forward, int currentPage,Func<int, string> pageUrl)
        {
            TagBuilder nextTag = new TagBuilder("a");
            nextTag.MergeAttribute("href", pageUrl(forward ? currentPage++ : currentPage--));
            nextTag.InnerHtml = forward? ">>" : "<<";
            nextTag.AddCssClass("btn btn-default");
            nextTag.MergeAttribute("data-ajax", "true");
            nextTag.MergeAttribute("data-ajax-mode", "replace");
            nextTag.MergeAttribute("data-ajax-update", "#todoPanel");
            nextTag.MergeAttribute("data-ajax-url", pageUrl(forward ? currentPage++ : currentPage--));
            return nextTag.ToString();
        }
    }
}
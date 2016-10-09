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
            return MvcHtmlString.Create(result.ToString());
        }
    }
}
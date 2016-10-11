using System;
using System.Collections.Generic;
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

            var pageItems = Pagination(pagingInfo.CurrentPage, pagingInfo.TotalPages);

            for (int i = 1; i <= pageItems.Count; i++)
            {
                if (pageItems[i - 1] == "...")
                {
                    var dotTag = new TagBuilder("button");
                    dotTag.InnerHtml = "...";
                    dotTag.AddCssClass("btn btn-default");
                    result.Append(dotTag);
                }
                else
                {
                    TagBuilder tag = new TagBuilder("a");
                    tag.MergeAttribute("href", pageUrl(int.Parse(pageItems[i - 1])));
                    tag.InnerHtml = pageItems[i - 1];
                    if (pageItems[i - 1] == pagingInfo.CurrentPage.ToString())
                    {
                        tag.AddCssClass("selected");
                        tag.AddCssClass("btn-primary");
                    }
                    tag.AddCssClass("btn btn-default");
                    tag.MergeAttribute("data-ajax", "true");
                    tag.MergeAttribute("data-ajax-mode", "replace");
                    tag.MergeAttribute("data-ajax-update", "#todoPanel");
                    tag.MergeAttribute("data-ajax-url", pageUrl(int.Parse(pageItems[i - 1])));
                    result.Append(tag);
                }
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

        private static List<string> Pagination(int c, int m)
        {
            var current = c;
            var last = m;
            var delta = 2;
            var left = current - delta;
            var right = current + delta + 1;
            var range = new int[m];
            var rangeWithDots = new string[m];
            var l = 0;

            for (var i = 1; i <= last; i++)
            {
                if (i == 1 || i == last || i >= left && i < right)
                {
                    range[i - 1] = i;
                }
            }

            foreach (var i in range)
            {
                if (l != 0)
                {
                    if (i - l == 2)
                    {
                        rangeWithDots[l] = (l + 1).ToString();
                    }
                    else if (i - l != 1)
                    {
                        rangeWithDots[l] = "...";
                    }
                }
                if (i == 0)
                {
                    continue;
                }
                rangeWithDots[i - 1] = i.ToString();
                l = i;
            }
            var outItems = new List<string>();

            foreach (var item in rangeWithDots)
            {
                if (item != null)
                {
                    outItems.Add(item);
                }
            }

            return outItems;
        }
    }
}
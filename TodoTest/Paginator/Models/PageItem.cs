namespace Paginator.Models
{
    public class PageItem
    {
        private readonly string _page;

        public PageItem(string page)
        {
            _page = page;
        }

        public string Page => _page;
    }
}

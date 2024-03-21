namespace testProject.Models
{
    public class Pager
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        public Pager()
        {

        }

        public Pager(int totalItems, int page, int pageSize = 10)
        {
            TotalItems = totalItems;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling((decimal)TotalItems / PageSize);
            CurrentPage = page;

            StartPage = Math.Max(1, CurrentPage - 2);
            EndPage = Math.Min(TotalPages, CurrentPage + 2);

            if (EndPage - StartPage < 4)
            {
                if (StartPage == 1)
                {
                    EndPage = Math.Min(TotalPages, StartPage + 4);
                }
                else if (EndPage == TotalPages)
                {
                    StartPage = Math.Max(1, EndPage - 4);
                }
            }
        }

    }
}

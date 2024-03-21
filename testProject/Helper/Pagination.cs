namespace testProject.Helper
{
    public static class Pagination
    {
        public static PagedData<T> PagedResult<T>(this List<T> list, int pageNumber, int pageSize) where T : class
        {
            var result = new PagedData<T>();
            result.Data=list.Skip(pageSize*(pageNumber-1)).Take(pageSize).ToList();
            result.TotalPages = Convert.ToInt32(Math.Ceiling((double)list.Count() / pageSize));
            result.CurrentPage = pageNumber;
            return result;
        }
    }
}

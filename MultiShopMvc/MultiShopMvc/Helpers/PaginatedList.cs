namespace MultiShopMvc.Helpers;

public class PaginatedList<T> : List<T>
{

    public PaginatedList(List<T> values , int totalelement , int elementsinpage ,int currentpage )
    {
        this.AddRange(values);
        TotalPage = (int)Math.Ceiling(totalelement / (double)elementsinpage);
        CurrentPage = currentpage;
        ElementInPage = elementsinpage;
    }


    public int TotalPage { get; set; }
    public int CurrentPage { get; set; }
    public bool HasNext { get => CurrentPage < TotalPage; }
    public bool HasPrevious { get => CurrentPage > 1 ; }
    public int ElementInPage { get; set; }


    public static PaginatedList<T> GetValues(IQueryable<T> query, int elementinpage, int currentpage)
    {
        var newquery = query.Skip((currentpage - 1) * elementinpage).Take(elementinpage);

        return new PaginatedList<T>(newquery.ToList(), query.Count(),elementinpage,currentpage);
    }

}

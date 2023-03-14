namespace API.Helpers
{
 public class PaginationHeader
 {
  private int pageSize;

  public PaginationHeader(int currentPage, int pageSize, int totalPages)
  {
   CurrentPage = currentPage;
   this.pageSize = pageSize;
   TotalPages = totalPages;
  }

  public PaginationHeader(int currentPage, int itemsPerPage, int totalItems, int totalPages)
  {
   CurrentPage = currentPage;
   ItemsPerPage = itemsPerPage;
   TotalItems = totalItems;
   TotalPages = totalPages; 
  }

  public int CurrentPage { get; set; }
  public int ItemsPerPage { get; set; }
  public int TotalItems { get; set; }
  public int TotalPages { get; set; }

 }
}
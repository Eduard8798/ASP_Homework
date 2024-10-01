namespace Classwork_7_Toyota_Color_Links_19_08.Models.ViewModels;

public class ApiPaginateResponse<TypeName>
{
    public IEnumerable<TypeName> Data { get; set; }
    public PaginateViewModel Paginate { get; set; }
}
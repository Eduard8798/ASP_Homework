using Microsoft.AspNetCore.Mvc.Rendering;

namespace Classwork_7_Toyota_Color_Links_19_08.Models
{
    public class PaginateViewModel
    {
        
        public List<string> Columns { get; set; } = new List<string>();
        public string SortColumn { get; set; } = "Name";
        
        public string SortDirection { get; set; } = "asc";
        public SelectList SortDirectionSelectedList { get; set; }
        public SelectList SortColumnSelectedList { get; set; }
        public int PageNumber { get; set; } = 1;  // Номер текущей страницы
        public int PageSize { get; set; } = 2;    // Количество элементов на странице
        public int TotalItems { get; set; }       // Общее количество элементов

        // Общее количество страниц (рассчитывается на основе общего количества элементов и размера страницы)
        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);

        // Возвращает true, если существует предыдущая страница
        public bool HasPreviousPage => PageNumber > 1;

        // Возвращает true, если существует следующая страница
        public bool HasNextPage => PageNumber < TotalPages;
    }
}
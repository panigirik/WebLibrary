using WebLibrary.Domain.Enums;

namespace WebLibrary.Domain.Filters;

public class PaginatedBookFilter
{
        /// <summary>
        /// Номер страницы.
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Размер страницы.
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Тип сортировки.
        /// </summary>
        public TypeOfSort TypeOfSort { get; set; } = TypeOfSort.Date;

        /// <summary>
        /// Категория книги.
        /// </summary>
        public Category BookCategory { get; set; } = Category.All;
}

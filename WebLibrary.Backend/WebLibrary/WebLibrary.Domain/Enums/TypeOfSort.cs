namespace WebLibrary.Domain.Enums;

/// <summary>
/// Типы сортировки для пагинации (в дальнейшем не только для пагинации)
/// </summary>
public enum TypeOfSort
{
    /// <summary>
    /// Сортировка по дате.
    /// </summary>
    Date = 1,

    /// <summary>
    /// Сортировка по автору.
    /// </summary>
    Author = 2,
    
    /// <summary>
    /// Сортировка по заголовку.
    /// </summary>
    Title = 3
}
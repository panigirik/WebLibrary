namespace WebLibrary.Domain.Enums;

/// <summary>
/// Категории, к которым могут относиться различные материалы.
/// </summary>
public enum Category
{
    /// <summary>
    /// Все категории.
    /// </summary>
    All = 0,

    /// <summary>
    /// Научная категория.
    /// </summary>
    Science = 1,

    /// <summary>
    /// Бизнес-категория.
    /// </summary>
    Business = 2,

    /// <summary>
    /// Категория для различных отраслей.
    /// </summary>
    Industries = 3
}
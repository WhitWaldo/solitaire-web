namespace SolitareWeb.Data;

/// <summary>
/// Identifies the focus of a clicked entity (so we can draw a highlight around it or the like) and confirm it's
/// the same object that's clicked again later.
/// </summary>
/// <param name="Target">The type of target that was the focus of the click.</param>
/// <param name="Index">The optional index (applicable only to the Foundation and Tableau) of the click.</param>
public sealed record ClickTarget(ClickTargetType Target, int? Index);

/// <summary>
/// The type of target that was clicked.
/// </summary>
public enum ClickTargetType
{
    Foundation,
    Tableau,
    DrawPile,
    HandPile
}
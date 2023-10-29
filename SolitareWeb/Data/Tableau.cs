namespace SolitareWeb.Data;

/// <summary>
/// The seven piles of cards that comprise the main table.
/// </summary>
public sealed record Tableau
{
    public Tableau()
    {
        //Initialize each pile with an empty stack of cards - that'll be handled during setup
        for (var a = 0; a <= 6; a++)
        {
            Piles.Add(new TableauPile(a));
        }
    }

    /// <summary>
    /// The game will initialize the 7 piles as empty and then populate each with their respective cards during the setup phase.
    /// </summary>
    public List<TableauPile> Piles { get; init; } = new();
}
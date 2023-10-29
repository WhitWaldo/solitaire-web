namespace SolitareWeb.Data;

/// <summary>
/// The collection of foundation piles on which a whole suit of sequenced cards must be built starting with
/// the Ace and finishing with the King.
/// </summary>
public sealed record Foundation
{
    /// <summary>
    /// The various foundation piles comprising the four suits.
    /// </summary>
    public List<FoundationPile> Piles = new()
    {
        new FoundationPile(Suit.Heart),
        new FoundationPile(Suit.Diamond),
        new FoundationPile(Suit.Spade),
        new FoundationPile(Suit.Club)
    };

    /// <summary>
    /// Validation check to see if the game has been won because all the foundation piles are completed.
    /// </summary>
    public bool IsGameWon => Piles.All(pile => pile.IsCompleted);
}
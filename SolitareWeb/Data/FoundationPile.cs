namespace SolitareWeb.Data;

/// <summary>
/// A pile of foundation cards representing the indicated suit.
/// </summary>
/// <param name="Suit">The suit of cards this foundation pile represents.</param>
public sealed record FoundationPile(Suit Suit)
{
    /// <summary>
    /// The various cards in the pile.
    /// </summary>
    public List<Card> Cards { get; init; } = new();

    /// <summary>
    /// Attempts to add the specified card to the pile. This will either return a true value and an empty string
    /// or it will return a false and a message indicating why the operation failed (generally this will be because a
    /// card add was attempted that's out of the anticipated order (A -> K) or the wrong suit.
    /// </summary>
    /// <param name="card">The card to add to the pile.</param>
    /// <returns></returns>
    public (bool successfullyAdded, string reason) TryAddToPile(Card card)
    {
        //Validate the card is the expected suit
        if (card.Suit != Suit)
        {
            return (false, "Wrong suit");
        }

        //Validate that the appropriate card was placed
        //If there are no cards in the pile, the card must have a face value of 0 (an ace)
        if (Cards.Count == 0 && card.FaceValue != 0)
        {
            return (false, "Wrong card order");
        }

        //Further, the card's face value must be one greater than the value already in the pile
        if (card.FaceValue != Cards.Last().FaceValue + 1)
        {
            return (false, "Wrong card order");
        }

        //Otherwise, good to go
        return (true, string.Empty);
    }

    /// <summary>
    /// Determines if the pile is completed as part of a win state check.
    /// </summary>
    /// <remarks>
    /// If we have 14 cards in the pile, we have all of them from A to K and per the validation rules above, we can
    /// be confident they're in the right order.
    /// </remarks>
    public bool IsCompleted => Cards.Count == 14;
}
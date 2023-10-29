namespace SolitareWeb.Data;

/// <summary>
/// Represents the stacked cards in the tableau pile.
/// </summary>
/// <param name="Offset">The zero-indexed offset of cards starting from the left side of the Tableau.</param>
public sealed record TableauPile(int Offset)
{
    /// <summary>
    /// The various cards in the pile stack (face up or down).
    /// </summary>
    public Stack<TableauCard> Cards { get; init; } = new();

    /// <summary>
    /// The top card in the pile, if any.
    /// </summary>
    public Card? TopCard =>
        Cards.TryPeek(out var topCard) ?
            //Return the top card in the pile without removing it
            topCard :
            //Indicates there are no cards left in the pile.
            null;
    
    /// <summary>
    /// Adds a card to the pile.
    /// </summary>
    /// <param name="card">The card being added.</param>
    public (bool successfullyAdded, string failureReason) AddToPile(TableauCard card)
    {  
        //There are several reasons this could fail:
        // - If there are any face-up cards on the pile and we attempt to add a face-down card
        if (Cards.Any(existingCard => existingCard.IsFaceUp) && !card.IsFaceUp)
            return (false, "Ineligible pile for a face-down card to be added");

        //If there are no cards in the pile already, we can add the card without issue
        if (!Cards.Any())
        {
            Cards.Push(card with {IsFaceUp = true});
            return (true, string.Empty);
        }

        //If all the cards are face-down already, accept the new card in whatever face up/down state it's in
        if (Cards.All(existingCard => !existingCard.IsFaceUp))
        {
            Cards.Push(card);
            return (true, string.Empty);
        }

        if (Cards.Any() && Cards.Last().IsFaceUp)
        {
            //The top-most card is face-up and an Ace
            if (Cards.Last().FaceValue == 0)
                return (false, "Cannot play a card on top of an Ace in a foundation pile");

            //The top-most card is face-up and the new card's value isn't an opposite color suit
            if (!IsOppositeSuitColor(Cards.Last().Suit, card.Suit))
                return (false, "Ineligible card for placement as suit must be opposite color");

            //The top-most card is face-up and the new card's face value isn't one less than the existing card's face value
            if (Cards.Last().FaceValue - card.FaceValue != 1)
                return (false,
                    "Ineligible card for placement as the placed card's value must be one less than the already shown value");
        }
        
        //Otherwise, accept the card in a face-up position
        Cards.Push(card with {IsFaceUp = true});
        return (true, string.Empty);
    }

    /// <summary>
    /// Determines if the two suits provided are opposite colors.
    /// </summary>
    /// <param name="suit1">The first suit to compare.</param>
    /// <param name="suit2">The second suit to compare.</param>
    /// <returns></returns>
    private static bool IsOppositeSuitColor(Suit suit1, Suit suit2) =>
        suit1 is Suit.Diamond or Suit.Heart && suit2 is Suit.Club or Suit.Spade ||
        suit2 is Suit.Diamond or Suit.Heart && suit1 is Suit.Club or Suit.Spade;
}

/// <summary>
/// Represents a card in a Tableau stack, this includes a flag for whether the card is face up or down.
/// All cards are initialized face down with the exception of the last one
/// </summary>
public sealed record TableauCard : Card
{
    /// <summary>
    /// Represents a card in a Tableau stack, this includes a flag for whether the card is face up or down.
    /// All cards are initialized face down with the exception of the last one
    /// </summary>
    /// <param name="card">The card represented.</param>
    /// <param name="isFaceUp">False if the card is face down (e.g. initialized there and not visible to the user) and True if the card is face up and part of an ordered stack (even if just a single card).</param>
    public TableauCard(Card card, bool isFaceUp) : base(card.FaceValue, card.Suit)
    {
        IsFaceUp = isFaceUp;
    }
    
    /// <summary>
    /// False if the card is face down (e.g. initialized there and not visible to the user) and True if the card is face up and part of an ordered stack (even if just a single card).
    /// </summary>
    public bool IsFaceUp { get; init; }
}
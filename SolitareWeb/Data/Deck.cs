namespace SolitareWeb.Data;

/// <summary>
/// Represents a deck of cards.
/// </summary>
public sealed record Deck
{
    /// <summary>
    /// Used for shuffling the numbers at initialization.
    /// </summary>
    private static readonly Random _rng = new();

    /// <summary>
    /// The various cards in the deck.
    /// </summary>
    public Stack<Card> Cards { get; private set; } = new();

    public Deck(bool buildStartingDeck)
    {
        //We only build the deck for the draw deck. The hand deck starts off empty.
        if (buildStartingDeck)
        {
            //Build the deck of possible cards
            var allCards = new List<Card>();
            for (uint a = 0; a <= 12; a++)
            {
                allCards.Add(new Card(a, Suit.Club));
                allCards.Add(new Card(a, Suit.Diamond));
                allCards.Add(new Card(a, Suit.Heart));
                allCards.Add(new Card(a, Suit.Spade));
            }

            //Now shuffle the deck a few times so the cards are randomly ordered
            for (var a = 0; a < 20; a++)
            {
                allCards = Shuffle(allCards);
            }

            //Feed each card into the stack in this type
            foreach (var card in allCards)
            {
                Cards.Push(card);
            }
        }
    }

    /// <summary>
    /// Used primarily be the game state when the draw deck is empty, this empties the hand deck so
    /// it can be reloaded back into the draw deck.
    /// </summary>
    /// <returns>A list of all the cards in this deck in FIFO order.</returns>
    public List<Card> EmptyStackToList()
    {
        var allCards = new List<Card>();
        while (Cards.TryPop(out var card))
            allCards.Add(card);
        return allCards;
    }

    /// <summary>
    /// Simple shuffling using the Fisher-Yates shuffle algorithm.
    /// </summary>
    /// <remarks>
    /// This works by iterating over the list from the end to the beginning and for each element, it
    /// selects a random element from the remaining un-shuffled elements and swaps it with the current element.
    /// This process is repeated until all elements have been shuffled.
    /// </remarks>
    /// <param name="cards">The cards to shuffle.</param>
    /// <returns></returns>
    private static List<Card> Shuffle(List<Card> cards)
    {
        var count = cards.Count;
        while (count > 1)
        {
            count--;
            var index = _rng.Next(count + 1);
            (cards[index], cards[count]) = (cards[count], cards[index]);
        }

        return cards;
    }

    
    /// <summary>
    /// Draws a number of cards from the "top" of the deck, removing them from this deck and returning them.
    /// </summary>
    /// <param name="cardCount">The number of cards to "draw".</param>
    /// <returns>The drawn cards, in order of draw.</returns>
    public List<Card> Draw(uint cardCount)
    {
        var tempStack = new List<Card>();
        for (var a = 0; a < cardCount; a++)
        {
            if (Cards.TryPop(out var drawnCard))
            {
                //If there's a card to draw, do so and put in the temp stack
                tempStack.Add(drawnCard);
            }
            else
            {
                //If there are no more cards to draw, return the temp stack as-is and leave it to the game to figure out what to do next
                return tempStack;
            }
        }

        return tempStack;
    }

    /// <summary>
    /// The opposite of the draw, this returns an ordered set of cards to this deck. 
    /// </summary>
    /// <remarks>
    /// There are two decks: Draw deck, hand deck. Typically, the cards will only be drawn from
    /// the draw deck as one shuffles through their hand (removed from the draw deck), they'll be discarded to the "hand deck" which
    /// represents the cards in the player's hand. When the game runs out of "draw deck" cards, the discard deck will be discarded to the
    /// draw deck in existing order. The draw deck should only be shuffled when it's initialized (e.g. when the game starts).
    /// </remarks>
    /// <param name="cards"></param>
    /// <returns></returns>
    public void DiscardTo(List<Card> cards)
    {
        foreach (var card in cards)
        {
            Cards.Push(card);
        }
    }
}
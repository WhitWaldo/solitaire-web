namespace SolitareWeb.Data;

public record GameState
{
    /// <summary>
    /// The cards comprising our draw deck.
    /// </summary>
    public Deck DrawDeck { get; init; } = new(true);

    /// <summary>
    /// The cards comprising our hand.
    /// </summary>
    public Deck HandDeck { get; init; } = new(false);

    /// <summary>
    /// The four foundation piles on which the sequenced suits of cards are built starting from the Ace
    /// and proceeding in order through the King.
    /// </summary>
    public Foundation Foundations { get; init; } = new();

    /// <summary>
    /// The seven piles of cards that comprise the game.
    /// </summary>
    public Tableau Tableau { get; init; } = new();

    /// <summary>
    /// Identifies the most recent element that was clicked by type and optionally offset.
    /// </summary>
    public ClickTarget? ClickTarget { get; init; } = null;

    /// <summary>
    /// Initializes the game state by performing several setup operations.
    /// </summary>
    public GameState()
    {
        //Our draw and hand decks are pre-initialized with the appropriate cards and the foundation is initialized empty
        //We simply need to draw enough cards from the draw deck and populate each of the piles in the Tableau to get started
        var startingPileOffset = 0;

        //Loop through until our starting pile offset equals 7 (recall that the seventh pile has an offset of 6 since it's zero-indexed)
        //and increment each loop so we start from one pile farther from the left
        while (startingPileOffset != 7)
        {
            //Starting from the startingPileOffset index, draw a card for each pile from left to right and place in the corresponding Tableau indexed pile
            for (var pileOffset = startingPileOffset; pileOffset < 7; pileOffset++)
            {
                //Draw one card from the "top" of the draw deck, removing it from the deck
                var drawnCard = DrawDeck.Draw(1).First(); //Gets the first of the stack since it's only a single draw

                //If the pileOffset is equal to the startingPileOffset, place the card face-up to start the face-up stack
                var isCardFaceUp = pileOffset == startingPileOffset;

                //Put it into the Tableau pile corresponding with the pileOffset index and in a face-up or -down position
                Tableau.Piles[pileOffset].AddToPile(new TableauCard(drawnCard, isCardFaceUp));
            }

            //Increment by one and repeat, if not equal to 7
            startingPileOffset += 1;
        }

        //That's all the setup we need - game on!
    }

    /// <summary>
    /// Attempts to draw three cards from the draw deck to place into the hand deck.
    /// </summary>
    /// <returns>The number of cards drawn from the draw deck.</returns>
    public int DrawFromDrawDeck()
    {
        //Draw up to three cards from the draw deck into a FIFO list
        var upToThreeCards = DrawDeck.Draw(3);
        
        //Place these cards into the hand deck in FILO order
        HandDeck.DiscardTo(upToThreeCards);

        //Return the numbers of cards pulled from the draw deck
        return upToThreeCards.Count;
    }

    /// <summary>
    /// The action that occurs when the user is out of cards in their draw deck and needs to recycle their hand deck back into the draw deck.
    /// </summary>
    public void RecycleHandDeckIntoDrawDeck()
    {
        //Empty out all the hand cards into FIFO order
        var allHandCards = HandDeck.EmptyStackToList();

        //Feed cards back into the draw deck
        DrawDeck.DiscardTo(allHandCards);
    }
}
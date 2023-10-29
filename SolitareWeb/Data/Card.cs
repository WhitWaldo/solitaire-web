namespace SolitareWeb.Data;

/// <summary>
/// Represents a single card.
/// </summary>
/// <param name="FaceValue">The numerical face value of the card, zero-indexed. For example, the Ace would typically be 1, so it has a value of 0 here and the King (highest value) would typically be 13, so it would have a value of 12.</param>
/// <param name="Suit">The suit of the card (hearts, diamonds, etc).</param>
public record Card(uint FaceValue, Suit Suit);
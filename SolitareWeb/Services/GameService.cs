using SolitareWeb.Data;

namespace SolitareWeb.Services;

public sealed class GameService
{
    /// <summary>
    /// Contains the current game state.
    /// </summary>
    public GameState _game { get; private set; } = new();

    /// <summary>
    /// Obliviates the existing game state and re-initializes it.
    /// </summary>
    public void StartNewGame()
    {
        //Destroy the old game and recreate it
        _game = new();
    }
}
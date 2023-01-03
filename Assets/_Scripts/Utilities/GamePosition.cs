using System;
using System.Collections.Generic;

public class GamePosition : IEquatable<GamePosition>
{
    public int CoinsPosition { get; }
    public int PlayerOneShapePosition { get; }
    public int PlayerTwoShapePosition { get; }

    public GamePosition(int coinsPosition, int playerOneShapePosition, int playerTwoShapePosition)
    {
        CoinsPosition = coinsPosition;
        PlayerOneShapePosition = playerOneShapePosition;
        PlayerTwoShapePosition = playerTwoShapePosition;
    }

    public bool Equals(GamePosition other)
    {
        return (
            this.CoinsPosition == other.CoinsPosition &&
            this.PlayerOneShapePosition == other.PlayerOneShapePosition &&
            this.PlayerTwoShapePosition == other.PlayerTwoShapePosition
        );
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeInfo
{
    public static MazeInfo mazeInfo;
    public static readonly int MAZE_EXTRA_SIZE = 3;

    public MazeSquare[,] squaresInfo;
    public float squareSize { get; private set; }
    public int mazeSize { get; private set; }

    public static Vector2Int playerStartSquareLocation;
    public static Vector2Int exitSquareLocation;

    /// <summary>
    /// Create a maze info using singleton pattern. 
    /// </summary>
    /// <param name="level">Player level. The value should be greater than zero. </param>
    /// <param name="squareSize">Square size of the maze. The value should be greater than zero. </param>
    public MazeInfo(int level, float squareSize)
    {
        // Syncronization to avoid the creation of two mazes at the same time
        lock(this)
        {
            // Singleton pattern
            if(mazeInfo == null && level > 0)
            {
                mazeSize = level + MAZE_EXTRA_SIZE;
                _InitialiseMazeSquaresInfo(mazeSize, squareSize);
                this.squareSize = squareSize;
                mazeInfo = this;
            }
        }
    }

    /// <summary>
    /// Creates the squares info with their default setting initilized.
    /// </summary>
    /// <param name="mazeSize">The size of the maze. </param>
    /// <param name="squareSize">The size of the square. </param>
    private void _InitialiseMazeSquaresInfo(int mazeSize, float squareSize)
    {
        squaresInfo = new MazeSquare[mazeSize, mazeSize];

        // Create the squares
        for(int x = 0; x < mazeSize; x++)
        {
            for(int y = 0; y < mazeSize; y++)
            {
                // Check for the cornors start
                // -----------------------------------
                if(x == 0 && y == 0)
                { // TOP_LEFT_CORNOR
                    squaresInfo[x, y] = new MazeSquare(squareSize, squareSize, SquarePosition.TOP_LEFT_CORNOR, x, y);
                }
                else if(x + 1 == mazeSize && y == 0)
                { // TOP_RIGHT_CORNOR
                    squaresInfo[x, y] = new MazeSquare(squareSize, squareSize, SquarePosition.TOP_RIGHT_CORNOR, x, y);
                }
                else if(x + 1 == mazeSize && y + 1 == mazeSize)
                { // BOTTOM_RIGHT_CORNOR
                    squaresInfo[x, y] = new MazeSquare(squareSize, squareSize, SquarePosition.BOTTOM_RIGHT_CORNOR, x, y);
                }
                else if(x == 0 && y + 1 == mazeSize)
                { // BOTTOM_LEFT_CORNOR
                    squaresInfo[x, y] = new MazeSquare(squareSize, squareSize, SquarePosition.BOTTOM_LEFT_CORNOR, x, y);
                }
                // -----------------------------------
                // Check for the cornors end

                // Check for the borders start
                // -----------------------------------
                else if (x == 0)
                { // LEFT_BORDER NOT CORNORS
                    squaresInfo[x, y] = new MazeSquare(squareSize, squareSize, SquarePosition.LEFT_BORDER, x, y);
                }
                else if (y == 0)
                { // TOP_BORDER NOT CORNORS
                    squaresInfo[x, y] = new MazeSquare(squareSize, squareSize, SquarePosition.TOP_BORDER, x, y);
                }
                else if (x + 1 == mazeSize)
                { // RIGHT_BORDER NOT CORNORS
                    squaresInfo[x, y] = new MazeSquare(squareSize, squareSize, SquarePosition.RIGHT_BORDER, x, y);
                }
                else if (y + 1 == mazeSize)
                { // BOTTOM_BORDER NOT CORNORS
                    squaresInfo[x, y] = new MazeSquare(squareSize, squareSize, SquarePosition.BOTTOM_BORDER, x, y);
                }
                // -----------------------------------
                // Check for the borders end

                else
                { // NOT CORNOR NOT BORDER
                    squaresInfo[x, y] = new MazeSquare(squareSize, squareSize, SquarePosition.CENTER, x, y);
                }
            }
        }
    }

    public static Vector2 GetSequareCenterPosition(Vector2Int squareIndex)
    {
        Debug.Log("Square Index: " + squareIndex);
        float squareSize = mazeInfo.squareSize;
        float groundSize = squareSize * mazeInfo.mazeSize;
        float x = squareSize / 2 + (squareIndex.x * squareSize) - groundSize / 2;
        float y = squareSize / 2 + (squareIndex.y * squareSize) - groundSize / 2;

        Debug.Log("Square Center Position: (" + x + ", " + y + ")" );
        return new Vector2(x, y);
    }

    public static Vector2 GetPlayerStartPosition()
    {
        return GetSequareCenterPosition(playerStartSquareLocation);
    }

    public static Vector2 GetExitPosition()
    {
        return GetSequareCenterPosition(exitSquareLocation);
    }
}

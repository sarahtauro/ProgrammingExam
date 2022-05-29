using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SquarePosition: byte
{
    TOP_LEFT_CORNOR = 0,
    TOP_RIGHT_CORNOR = 1,
    BOTTOM_RIGHT_CORNOR = 2,
    BOTTOM_LEFT_CORNOR = 3,
    LEFT_BORDER = 4,
    TOP_BORDER = 5,
    RIGHT_BORDER = 6,
    BOTTOM_BORDER = 7,
    CENTER = 8
}


public class MazeSquare
{
    // Initializing defualt info
    //-------------------------------------
    // Square walls
    private bool _isLeftOpen = false;
    private bool _isTopOpen = false;
    private bool _isRightOpen = false;
    private bool _isBottomOpen = false;

    // Open Direction
    private Wall _openDirection = Wall.NONE;
    public bool hasBeenFound = false;

    // Closed Directions
    private Wall[] _closedDirections;

    // Location
    private Vector2Int _location;

    // Square size
    private float _width = 1;
    private float _length = 1;
    //-------------------------------------


    // Constructors
    public MazeSquare(float width, float length)
    {
        _width = width;
        _length = length;
    }

    public MazeSquare(float width, float length, SquarePosition position) : this(width, length)
    {
        switch(position)
        {
            // Cornors case start
            //--------------------------------------------------------
            /* If the square is on the top-left cornor, then the top border
            * and the left border are closing two direction */
            case SquarePosition.TOP_LEFT_CORNOR:
                _closedDirections = new Wall[]
                {
                    Wall.LEFT,
                    Wall.TOP
                };
                break;
            /* If the square is on the top-right cornor, then the top border
            * and the right border are closing two direction */
            case SquarePosition.TOP_RIGHT_CORNOR:
                _closedDirections = new Wall[]
                {
                    Wall.RIGHT,
                    Wall.TOP
                };
                break;
            /* If the square is on the bottom-left cornor, then the bottom border
            * and the left border are closing two direction */
            case SquarePosition.BOTTOM_LEFT_CORNOR:
                _closedDirections = new Wall[]
                {
                    Wall.LEFT,
                    Wall.BOTTOM
                };
                break;
            /* If the square is on the bottom-right cornor, then the bottom border
            * and the right border are closing two direction */
            case SquarePosition.BOTTOM_RIGHT_CORNOR:
                _closedDirections = new Wall[]
                {
                    Wall.RIGHT,
                    Wall.BOTTOM
                };
                break;
            //--------------------------------------------------------
            // Cornors case end

            // Edges(LEFT, TOP, RIGHT, BOTTOM) case start
            //--------------------------------------------------------
            /* The left border is closing the left direction of this square */
            case SquarePosition.LEFT_BORDER:
                _closedDirections = new Wall[]
                {
                    Wall.LEFT
                };
                break;
            /* The top border is closing the top direction of this square */
            case SquarePosition.TOP_BORDER:
                _closedDirections = new Wall[]
                {
                    Wall.TOP
                };
                break;
            /* The right border is closing the right direction of this square */
            case SquarePosition.RIGHT_BORDER:
                _closedDirections = new Wall[]
                {
                    Wall.RIGHT
                };
                break;
            /* The bottom border is closing the bottom direction of this square */
            case SquarePosition.BOTTOM_BORDER:
                _closedDirections = new Wall[]
                {
                    Wall.BOTTOM
                };
                break;
            // Edges(LEFT, TOP, RIGHT, BOTTOM) cases end
            //--------------------------------------------------------
        }
    }

    public MazeSquare(float width, float length, SquarePosition position, Vector2Int location) : this(width, length, position)
    {
        _location = location;
    }

    public MazeSquare(float width, float length, SquarePosition position, int x, int y)
        : this(width, length, position, new Vector2Int(x, y))
    {

    }

    // Methods
    //-------------------------------------


    // *OpenDirection Methods*
    //************************************
    /// <summary>
    /// Checks whether a direction has been assigned to this square. 
    /// </summary>
    /// <returns>Whether a direction is assigned</returns>
    public bool hasOpenDirection()
    {
        return _openDirection != Wall.NONE;
    }


    
    /// <summary>
    /// Assign a direction to a square. Throws an ArgumentException in case a Wall.NONE is 
    /// being assigned as a direction.
    /// </summary>
    /// <param name="direction">The opening direction of this area.</param>
    public void AssignOpenDirection(Wall direction)
    {
        if (direction == Wall.NONE)
            throw new ArgumentException("Can NOT assign Wall.NONE as a direction!");
        _openDirection = direction;
    }

    /// <summary>
    /// Force assigning a random open direction.
    /// </summary>
    public void ForceAssignRandomOpenDirection()
    {
        bool hasChoseDir;
        Wall dir;
        do
        {
            hasChoseDir = true;
            dir = _wallsDirections[UnityEngine.Random.Range(0, _wallsDirections.Length)];
            if (_closedDirections != null)
            { // There is some borders that closed some of the directions
                for (int i = 0; i < _closedDirections.Length; i++)
                {
                    if (dir == _closedDirections[i])
                    {
                        hasChoseDir = false;
                        break;
                    }
                }
            }


        } while (!hasChoseDir);

        AssignOpenDirection(dir);
    }

    /// <summary>
    /// Get the open direction of this square. 
    /// </summary>
    /// <returns>The open direction, Can be Wall.NONE if it is not assigned. </returns>
    public Wall GetOpenDirection()
    {
        return _openDirection;
    }
    //************************************
    // Ends Of OpenDirection Methods



    // *Walls Methods*
    //************************************
    /// <summary>
    /// Open of the square walls directions. Throws an ArgumentExceotion in case a Wall.NONE is 
    /// recived as an argument. 
    /// </summary>
    /// <param name="direction">The direction to indicate as open.</param>
    public void OpenAWall(Wall direction)
    {
        // Avoid opening closed walls
        bool argumentException = false;

        // Check if the wall to be opened is closed by a border
        if(_closedDirections != null)
        { // Check if there is walls closed by the borders
            for(int i = 0; i < _closedDirections.Length; i++)
            {
                if(direction == _closedDirections[i])
                { // This wall is closed by a border => Can not be opened
                    argumentException = true;
                    break;
                }
            }
        }

        if (direction == Wall.NONE ||argumentException)
            throw new ArgumentException("Can NOT open Wall.NONE since it is not a direction!");

        // TODO: Limit the open direction that can be opened

        switch(direction)
        {
            case Wall.LEFT: // 0
            // case Wall.WEST: // 0
                _isLeftOpen = true;
                break;
            case Wall.TOP: // 1
            // case Wall.FORWARD: // 1
            // case Wall.NORTH: // 1
                _isTopOpen = true;
                break;
            case Wall.RIGHT: // 2
                _isRightOpen = true;
                break;
            case Wall.BOTTOM: // 3
            // case Wall.BACKWARD: // 3
            // case Wall.SOUTH: // 3
                _isBottomOpen = true;
                break;
        }
    }

    /// <summary>
    /// Checks if at least one of the walls is open.
    /// </summary>
    /// <returns>True if at least one wall is open</returns>
    public bool HasOpenWalls()
    {
        bool hasOpenWalls = false;

        if (_isLeftOpen || _isTopOpen || _isRightOpen || _isBottomOpen)
            hasOpenWalls = true;

        return hasOpenWalls;
    }

    /// <summary>
    /// Counts the numbers of walls that are open in this area. 
    /// </summary>
    /// <returns>The number of walls that are open. </returns>
    public int GetOpenWallsCount()
    {
        int wallsOpen = 0;

        if (_isLeftOpen)
            wallsOpen++;
        if (_isTopOpen)
            wallsOpen++;
        if (_isRightOpen)
            wallsOpen++;
        if (_isBottomOpen)
            wallsOpen++;

        return wallsOpen;
    }

    /// <summary>
    /// Checks whether the left direction is open or is closed by the border
    /// </summary>
    /// <returns>True if the direction is open</returns>
    public bool IsLeftOpen()
    {
        bool isOpen = _isLeftOpen;
        // Check if the square has walls closed by the borders
        if (_closedDirections != null)
        {
            for (int i = 0; i < _closedDirections.Length; i++)
            {
                // Check 
                if(_closedDirections[i] == Wall.LEFT)
                {
                    isOpen = false;
                    break;
                }
            }
        }

        return isOpen;
    }

    public bool IsWallOpen(Wall wallDirection)
    {
        bool isOpen = true;

        // TODO: Check if this needs to be uncommented
        /*switch(wallDirection)
        {
            case Wall.LEFT: // 0
            // case Wall.WEST: // 0
                isOpen = _isLeftOpen;
                break;
            case Wall.TOP: // 1
            // case Wall.FORWARD: // 1
            // case Wall.NORTH: // 1
                isOpen = _isTopOpen;
                break;
            case Wall.RIGHT: // 2
                isOpen = _isRightOpen;
                break;
            case Wall.BOTTOM: // 3
            // case Wall.BACKWARD: // 3
            // case Wall.SOUTH: // 3
                isOpen = _isBottomOpen = true;
                break;
        }*/

        // This part is placed beneth because its result is more important
        // Check if the square has walls closed by the borders
        if (_closedDirections != null)
        {
            for (int i = 0; i < _closedDirections.Length; i++)
            {
                // Check if this direction is closed by a border
                if (_closedDirections[i] == wallDirection)
                {
                    isOpen = false;
                    break;
                }
            }
        }

        return isOpen;
    }

    public Wall GetOppositeWall(Wall direction)
    {
        Wall oppositeWall = Wall.NONE;

        switch(direction)
        {
            case Wall.LEFT:
                oppositeWall = Wall.RIGHT;
                break;
            case Wall.TOP:
                oppositeWall = Wall.BOTTOM;
                break;
            case Wall.RIGHT:
                oppositeWall = Wall.LEFT;
                break;
            case Wall.BOTTOM:
                oppositeWall = Wall.TOP;
                break;
        }

        return oppositeWall;
    }

    private static Wall[] _wallsDirections = new Wall[]
    {
        Wall.LEFT,
        Wall.TOP,
        Wall.RIGHT,
        Wall.BOTTOM
    };

    public Wall[] GetUnchoosenDirections()
    {
        Wall[] unchoosenDirections = null;

        // Check if the left wall is open
        bool leftWall = IsWallOpen(Wall.LEFT);

        // Loop around all the walls and check if they are open
        for(int i = 0; i < _wallsDirections.Length; i++)
        {
            /* This important to avoid choosing the same direction
             * in case the maze creator has reached deadend and returned
             * back to this square to choose another road */
            // Check if this wall direction is chosen
            if (_wallsDirections[i] == _openDirection)
                continue;

            // Check if a this wall is open
            bool isWallOpen = IsWallOpen(_wallsDirections[i]);

            // If this direction is not closed check if the square in the left
            // has a direction pointing toward this one
            if (isWallOpen)
            {
                // Get the location of the sqaure this direction is pointing towards
                Vector2Int nextSquareLocation = GetSquareAtDirection(_wallsDirections[i]);

                // Debug.Log(MazeInfo.mazeInfo.squaresInfo.GetLength(1));

                // Not necessary check
                // Check if this location exists
                if (nextSquareLocation.x < 0 || nextSquareLocation.x >= MazeInfo.mazeInfo.squaresInfo.GetLength(0)
                    || nextSquareLocation.y < 0 || nextSquareLocation.y >= MazeInfo.mazeInfo.squaresInfo.GetLength(1))
                { // Location out of bounds

                }
                else
                { // There is a square next to this one
                  // Check the opposite wall of the next square
                  // |This Square ->||<- Opposite wall of next square|
                    MazeSquare nextSquare =
                        MazeInfo.mazeInfo.squaresInfo[nextSquareLocation.x, nextSquareLocation.y];
                    // Check if the next area was already found before
                    if(/*GetOppositeWall(_wallsDirections[i]) != */nextSquare.GetOpenDirection() == Wall.NONE)
                    { // An open direction was found
                        // The next square open is pointing to another square than this one
                        if(unchoosenDirections == null)
                        { // This is the first unchoosenDirection that were found
                            unchoosenDirections = new Wall[] { _wallsDirections[i] };
                        }
                        else
                        { // There is other unchoosenDirection that were found
                            Wall[] newArray = new Wall[unchoosenDirections.Length + 1];
                            
                            // Save the old unchoosen direction to the array
                            for(int wallI = 0; wallI < unchoosenDirections.Length; wallI++)
                            {
                                newArray[wallI] = unchoosenDirections[wallI];
                            }

                            // Add the new unchoosen direction
                            newArray[newArray.Length - 1] = _wallsDirections[i];
                            // Assign the new unchoosen directions to the main array
                            unchoosenDirections = newArray;
                        }
                    }
                }
            }
        }
        
        return unchoosenDirections;
    }


    /// <summary>
    /// Get the square next to this square that the parameter
    /// direction is pointing towards. 
    /// </summary>
    /// <param name="direction">The direction ponting direction. </param>
    /// <returns>The square that the parameter direction pointing towards from this square. </returns>
    public Vector2Int GetSquareAtDirection(Wall direction)
    {
        Vector2Int location = new Vector2Int(_location.x, _location.y);

        switch(direction)
        {
            case Wall.LEFT:
                location.x -= 1;
                break;
            case Wall.TOP:
                location.y -= 1;
                break;
            case Wall.RIGHT:
                location.x += 1;
                break;
            case Wall.BOTTOM:
                location.y += 1;
                break;
        }

        return location;
    }

    /// <summary>
    /// Checks if at least one of the squares around this one
    /// has their open direction pointing towards this one.
    /// </summary>
    /// <returns>Whether there is a square pointing towards this area. </returns>
    public bool HasSquarePointingToward()
    {
        bool hasSquarePointingAtIt = false;

        // Run around all the open directions.
        for(int i = 0; i < _wallsDirections.Length; i++)
        {
            Wall dir = _wallsDirections[i];
            // Get the position of the square that this direction is pointing towards
            Vector2Int squareAtDirLocation = GetSquareAtDirection(dir);
            // Check if this location exists
            if (squareAtDirLocation.x < 0 || squareAtDirLocation.x >= MazeInfo.mazeInfo.squaresInfo.GetLength(0)
                || squareAtDirLocation.y < 0 || squareAtDirLocation.y >= MazeInfo.mazeInfo.squaresInfo.GetLength(1))
            { // Location out of bounds

            }
            else
            { // This square exists

                // Get the square
                MazeSquare squareAtDir = MazeInfo.mazeInfo.squaresInfo[squareAtDirLocation.x, squareAtDirLocation.y];

                // Check if the square at the direction has the openDirection pointing towards this one
                if(squareAtDir.GetOpenDirection() == GetOppositeWall(dir))
                { // A square that has opening to this one is found

                    // Indicate that a square with opening to this one is fiund
                    hasSquarePointingAtIt = true;
                    // No need to search for other squares with opening to this one
                    break;
                }
            }
        }

        return hasSquarePointingAtIt;
    }

    //************************************
    // Ends Of Walls Methods

    /// <summary>
    /// Gets the size of the square as a 2D vector 
    /// where the Vector2.x is the width and
    /// the Vector2.y is the length of the square. 
    /// </summary>
    /// <returns></returns>
    public Vector2 GetSize()
    {
        return new Vector2(_width, _length);
    }

    //-------------------------------------
}

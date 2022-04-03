using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    [SerializeField]
    [Tooltip("This is which type of piece the chess piece is.")]
    private Piece_Type PieceType;

    [SerializeField]
    [Tooltip("This is which type of piece the chess piece is.")]
    private Piece_Color PieceColor;

    [SerializeField]
    [Tooltip("This is every single chess board spot.")]
    private ChessBoard[] ChessBoardSpots;

    private int CurrentXPosition, CurrentYPosition;

    private bool JustMoved;

    enum Piece_Type
    {
        Pawn,
        King,
        Rook
    };

    enum Piece_Color
    {
        Black,
        White
    }

    void OnTriggerEnter(Collider other)
    {
        if ( other != null )
        {
            if ( other.CompareTag("BoardTile"))
            {
                ChessBoard temp = other.GetComponent<ChessBoard>();
                CurrentXPosition = temp.GetXValue();
                CurrentYPosition = temp.GetYValue();
                if ( JustMoved )
                {
                    FindValidMoves();
                    JustMoved = false;
                }
            }
        }
    }

    public bool IsValidColor()
    {
        switch (PieceColor)
        {
            case Piece_Color.Black:
            return false;
            
            default:
            return true;
        }
    }

    public void DeselectPiece()
    {
        foreach (ChessBoard spot in ChessBoardSpots)
        {
            spot.Darken();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToTile(Vector3 newPosition)
    {
        gameObject.transform.position = newPosition;
        gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y + 0.1f, gameObject.transform.position.z);
        JustMoved = true;
    }

    public int GetCurrentXPosition()
    {
        return CurrentXPosition;
    }

    public int GetCurrentYPosition()
    {
        return CurrentYPosition;
    }

    public void FindValidMoves()
    {
        switch(PieceType)
        {
            case Piece_Type.King:
            foreach (ChessBoard spot in ChessBoardSpots)
            {
                int x = spot.GetXValue(), y = spot.GetYValue();
                
                if ( (!spot.GetHasWhiteChessPieceOnIt()) && ( x == (CurrentXPosition + 1) || x == (CurrentXPosition - 1) || x == CurrentXPosition ) && ( y == (CurrentYPosition + 1) || y == (CurrentYPosition - 1) || y == CurrentYPosition ) )
                {
                    spot.LightUp();
                }
                else
                {
                    spot.Darken();
                }
            }
            break;

            case Piece_Type.Pawn:
            foreach (ChessBoard spot in ChessBoardSpots)
            {
                int x = spot.GetXValue(), y = spot.GetYValue();
                
                if ( (!spot.GetHasWhiteChessPieceOnIt()) && ( x == CurrentXPosition && y == (CurrentYPosition + 1) ) )
                {
                    spot.LightUp();
                }
                else
                {
                    spot.Darken();
                }
            }
            break;

            case Piece_Type.Rook:
            foreach (ChessBoard spot in ChessBoardSpots)
            {
                int x = spot.GetXValue(), y = spot.GetYValue();

                /*
                int invalidX = 0, invalidY = 0;

                if ( spot.GetHasWhiteChessPieceOnIt() )
                {
                    invalidX = spot.GetXValue();
                    invalidY = spot.GetYValue();
                    Debug.Log($"Setting invalid X to {invalidX} and invalid Y to {invalidY}");
                }

                (CurrentXPosition != invalidX || CurrentYPosition != invalidY) && 

                */
                
                if ( ( x == CurrentXPosition || y == CurrentYPosition ) )
                {
                    spot.LightUp();
                }
                else
                {
                    spot.Darken();
                }
            }
            break;
            /*
            case Piece_Type.Bishop:
            foreach (ChessBoard spot in ChessBoardSpots)
            {
                int x = spot.GetXValue(), y = spot.GetYValue();
                
                if ( (!spot.GetHasWhiteChessPieceOnIt()) && ( x == CurrentXPosition && y == (CurrentYPosition + 1) ) )
                {
                    spot.LightUp();
                }
                else
                {
                    spot.Darken();
                }
            }
            break;
            */
        }
    }
}

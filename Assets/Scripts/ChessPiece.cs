using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    [SerializeField]
    [Tooltip("This is which type of piece the chess piece is.")]
    private Piece_Type PieceType;

    [SerializeField]
    [Tooltip("This is every single chess board spot.")]
    private ChessBoard[] ChessBoardSpots;

    private int CurrentXPosition, CurrentYPosition;

    enum Piece_Type
    {
        Pawn,
        King
    };

    void OnTriggerEnter(Collider other)
    {
        if ( other != null )
        {
            Debug.Log("Landed on " +other.gameObject.name);
            if ( other.CompareTag("BoardTile"))
            {
                ChessBoard temp = other.GetComponent<ChessBoard>();
                CurrentXPosition = temp.GetXValue();
                CurrentYPosition = temp.GetYValue();
            }
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
                Debug.Log($"{x}, {y}, {CurrentXPosition}, {CurrentYPosition}");
                
                if ( ( x == (CurrentXPosition + 1) || x == (CurrentXPosition - 1) || x == CurrentXPosition ) && ( y == (CurrentYPosition + 1) || y == (CurrentYPosition - 1) || y == CurrentYPosition ) )
                {
                    Debug.Log($"{x}, {y}, {CurrentXPosition}, {CurrentYPosition}");
                    spot.LightUp();
                }
                else
                {
                    spot.Darken();
                }
            }
            break;
        }
    }
}

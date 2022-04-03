using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Rook,
        Bishop
    };

    enum Piece_Color
    {
        Black,
        White
    }

    private GameManager Game_Manager;

    void Start()
    {
        Game_Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
                    //FindValidMoves();
                    if ( IsValidColor() )
                    StartCoroutine("Enemy_Move");
                    JustMoved = false;
                }
            }
            if ( other.CompareTag("ChessPiece"))
            {
                Destroy (other.gameObject);
            }
        }
    }

    private IEnumerator Enemy_Move()
    {
        yield return new WaitForSeconds(0.1f);
        EnemyMove();
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
        Debug.Log("Deselecting pieces...");
        foreach (ChessBoard spot in ChessBoardSpots)
        {
            spot.Darken();
        }
    }

    public void MoveToTile(Vector3 newPosition)
    {
        /*
        foreach (ChessBoard spot in ChessBoardSpots)
        {
            if (spot.GetXValue() == CurrentXPosition && spot.GetYValue() == CurrentYPosition)
            {
                spot.HasWhiteChessPieceOnIt = false;
                spot.HasBlackChessPieceOnIt = false;
            }
        }
        */
        gameObject.transform.position = newPosition;
        gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y + 0.1f, gameObject.transform.position.z);
        JustMoved = true;    
        if ( IsValidColor() )       
        StartCoroutine("SubtractNumberOfMoves");
    }

    private IEnumerator SubtractNumberOfMoves()
    {
        yield return new WaitForSeconds(0.4f);
        Game_Manager.NumberOfMoves--;
    }

    public int GetCurrentXPosition()
    {
        return CurrentXPosition;
    }

    public int GetCurrentYPosition()
    {
        return CurrentYPosition;
    }

    public void EnemyMove()
    {
        Debug.Log(Game_Manager.GetNumberOfMoves());
        if ( Game_Manager.GetNumberOfMoves() == 3 )
        {
            if ( CurrentXPosition == 1 && CurrentYPosition == 6 && Game_Manager.CorrectMoves == 0)
            {
                Game_Manager.CorrectMoves++;
            }
                GameObject temp = GameObject.Find("Black Pawn (1)");
                if ( temp != null && !GameObject.Find("Board_Tile (21)").GetComponent<ChessBoard>().HasWhiteChessPieceOnIt)
                {
                    temp.GetComponent<ChessPiece>().MoveToTile(new Vector3 (temp.transform.position.x, temp.transform.position.y, temp.transform.position.z - 1.34f));
                }
                else if (temp != null && GameObject.Find("Board_Tile (21)").GetComponent<ChessBoard>().HasWhiteChessPieceOnIt)
                {
                    GameObject temp3 = GameObject.Find("Black Pawn");
                    temp3.GetComponent<ChessPiece>().MoveToTile(new Vector3 (temp3.transform.position.x, temp3.transform.position.y, temp3.transform.position.z - 1.34f));
                }
                else
                {
                    GameObject temp2 = GameObject.Find("Black Rook");
                    if ( temp2 != null )
                    {
                        temp2.GetComponent<ChessPiece>().MoveToTile(new Vector3 (temp2.transform.position.x, temp2.transform.position.y, temp2.transform.position.z - 1.34f));
                    }
                }                
        }
        else if ( Game_Manager.GetNumberOfMoves() == 2 )
        {
            if ( CurrentXPosition == 6 && CurrentYPosition == 6 && Game_Manager.CorrectMoves == 1)
            {
                Game_Manager.CorrectMoves++;
            }

            GameObject temp = GameObject.Find("Black King");
            Debug.Log(temp.name);
                if ( temp != null && GameObject.Find("Black Rook") == null)
                {
                    Debug.Log("addsfsda");
                    temp.GetComponent<ChessPiece>().MoveToTile(new Vector3 (temp.transform.position.x - 1.34f, temp.transform.position.y, temp.transform.position.z));
                }
                else if (temp != null && GameObject.Find("Board_Tile (6)").GetComponent<ChessBoard>().HasBlackChessPieceOnIt)
                {
                    GameObject temp3 = GameObject.Find("Black Pawn (1)");

                    if ( temp3 != null && GameObject.Find("Board_Tile (28)").GetComponent<ChessBoard>().HasWhiteChessPieceOnIt)
                    {
                        Debug.Log("ladisdfo");
                        temp3.GetComponent<ChessPiece>().MoveToTile(new Vector3 (temp3.transform.position.x - 1.34f, temp3.transform.position.y, temp3.transform.position.z - 1.34f));
                    }
                    
                    else
                    {
                        GameObject temp2 = GameObject.Find("Black Rook");
                        if ( temp2 != null )
                        {
                            temp2.GetComponent<ChessPiece>().MoveToTile(new Vector3 (temp2.transform.position.x, temp2.transform.position.y, temp2.transform.position.z - 1.34f));
                        }
                    }
                }      
                
        }
        else if ( Game_Manager.GetNumberOfMoves() == 1 )
        {
            if ( CurrentXPosition == 1 && CurrentYPosition == 8 && Game_Manager.CorrectMoves == 2)
            {
                Debug.Log("Yate");
                Game_Manager.CorrectMoves++;
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
                
        }
    }

    public void FindValidMoves()
    {
        if ( IsValidColor() )
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
            
            case Piece_Type.Bishop:
            foreach (ChessBoard spot in ChessBoardSpots)
            {
                int x = spot.GetXValue(), y = spot.GetYValue();
                
                if ( (!spot.GetHasWhiteChessPieceOnIt()) && ( ( x - CurrentXPosition == y - CurrentYPosition) || ( Mathf.Abs(x - CurrentXPosition) == Mathf.Abs(y - CurrentYPosition)) ) )
                {
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
        
}

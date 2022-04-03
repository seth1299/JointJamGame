using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{
    [SerializeField]
    private int CoordinateXValue, CoordinateYValue;

    [SerializeField]
    private GameObject Particle_System;

    private GameObject BlackPiece = null, WhitePiece = null;

    public bool IsLitUp, HasWhiteChessPieceOnIt, HasBlackChessPieceOnIt;

    public int GetXValue()
    {
        return CoordinateXValue;
    }

    public int GetYValue()
    {
        return CoordinateYValue;
    }

    public void LightUp()
    {
        Particle_System.SetActive(true);
        IsLitUp = true;
    }

    public void Darken()
    {
        Particle_System.SetActive(false);
        IsLitUp = false;
    }

    public bool GetIsLitUp()
    {
        return IsLitUp;
    }

    public bool GetHasWhiteChessPieceOnIt()
    {
        return HasWhiteChessPieceOnIt;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( other != null )
        {
            if ( other.gameObject.CompareTag("ChessPiece") && other.gameObject.GetComponent<ChessPiece>().IsValidColor())
            {
                HasWhiteChessPieceOnIt = true;
                WhitePiece = other.gameObject;
                if ( HasBlackChessPieceOnIt )
                {
                    if (BlackPiece.name == "Black King")
                    {
                        GameObject.Find("GameManager").GetComponent<GameManager>().CorrectMoves = 3;
                    }
                    Destroy(BlackPiece);
                    HasBlackChessPieceOnIt = false;
                }
            }
            else if ( other.gameObject.CompareTag("ChessPiece") && !other.gameObject.GetComponent<ChessPiece>().IsValidColor())
            {
                HasBlackChessPieceOnIt = true;
                if ( HasWhiteChessPieceOnIt )
                    Destroy(WhitePiece);
                BlackPiece = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ( other != null )
        {
            if ( other.gameObject.CompareTag("ChessPiece") && other.gameObject.GetComponent<ChessPiece>().IsValidColor())
            {
                HasWhiteChessPieceOnIt = false;
            }
            else if ( other.gameObject.CompareTag("ChessPiece") && !other.gameObject.GetComponent<ChessPiece>().IsValidColor())
            {
                HasBlackChessPieceOnIt = true;
            }
        }
    }

}

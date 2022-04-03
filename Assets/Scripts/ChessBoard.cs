using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{
    [SerializeField]
    private int CoordinateXValue, CoordinateYValue;

    [SerializeField]
    private GameObject Particle_System;

    private bool IsLitUp, HasWhiteChessPieceOnIt;

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
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{
    [SerializeField]
    private int CoordinateXValue, CoordinateYValue;

    [SerializeField]
    private GameObject Particle_System;

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
    }

    public void Darken()
    {
        Particle_System.SetActive(false);
    }

}

using System;
using UnityEngine;

public enum BalloonColor
{
    Red,
    Blue,
    Green,
    Yellow,
    Black
}

[CreateAssetMenu(fileName = "BalloonData", menuName = "Scriptable Objects/BalloonData")]
public class BalloonData : ScriptableObject
{
    public BalloonColor balloonColor;
    public int points;
}

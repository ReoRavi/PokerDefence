using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

    // Club, Heart, Spade, Diamond
    public int shape;
    // A, 2, 7, 8, 9, 10, J, Q, K
    public int number;

    public void Init(int shape, int number)
    {
        this.shape = shape;
        this.number = number;
    }

    public int GetSpriteCount()
    {
        return shape * number;
    }
}

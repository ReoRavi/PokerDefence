using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    public Sprite[] sprites;
    public GameObject tile;

    public void CreateMapTiles()
    {
        int yCount = 0;

        for (int y = 0; y < 6; y++)
        {
            if (y == 1 || y == 5)
            {
                yCount += 3;
            }

            int xCount = 0;

            for (int x = 0; x < 11; x++)
            {
                if (x == 1 || x == 10)
                {
                    xCount++;
                }

                GameObject obj = Instantiate(tile);
                obj.GetComponent<SpriteRenderer>().sprite = sprites[0 + xCount + yCount];
                obj.transform.position = new Vector3(-6.1F + (x * 1.23F), 4.1F - (y * 1.23F), 0);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationOne : MonoBehaviour
{
    public Transform floors;
    public Transform walls;
    public Transform columns;
    public Transform seeThroughWalls;

    public GameObject floorPrefab;
    public GameObject wallPrefab;
    public GameObject columnPrefab;
    public GameObject seeThroughWallPrefab;


    public int xSize = 6;
    public int zSize = 6;

    int xCutoutSize = 2;
    int zCutoutSize = 2;

    int prefabSize = 5;

    // Start is called before the first frame update
    private void Awake()
    {
        Generation();
    }

    // Update is called once per frame

    void Generation()
    {                                                  
        Vector3 targetPos;
        GameObject newObject;

        for (int x = 0; x < xSize; x++)
        {
            // x wall
            targetPos = new Vector3(x * prefabSize, 0, 0);
            newObject = Instantiate(wallPrefab, targetPos, Quaternion.identity);
            newObject.transform.parent = walls;

            // x column
            targetPos = new Vector3(x * prefabSize, 0, 0.25f);
            newObject = Instantiate(columnPrefab, targetPos, Quaternion.identity);
            newObject.transform.parent = columns;

            for (int z = 0; z < zSize; z++)
            {                                                       // floors
                targetPos = new Vector3(x * prefabSize, 0, z * prefabSize);
                newObject = Instantiate(floorPrefab, targetPos, Quaternion.identity);
                newObject.transform.parent = floors;

                if(x == 0)  // y wall
                {
                    targetPos.x = -5;
                    newObject = Instantiate(wallPrefab, targetPos, Quaternion.identity);
                    newObject.transform.localEulerAngles = new Vector3(0,90,0); 
                    newObject.transform.parent = walls;

                    // y column
                    targetPos.x = -4.75f;
                    if(z == 0)                      // first column
                    {
                        targetPos.z = 0.25f;
                    }

                    newObject = Instantiate(columnPrefab, targetPos, Quaternion.identity);
                    newObject.transform.parent = columns;

                    if (z == zSize - 1)         // final column
                    {
                        targetPos.z += prefabSize;
                        newObject = Instantiate(columnPrefab, targetPos, Quaternion.identity);
                        newObject.transform.parent = columns;
                    }
                }
            }
        }

        for(int x = 0; x < xSize*2;  x++)
        {
            targetPos = new Vector3(x * (prefabSize * 0.5f) -5, 0, zSize * prefabSize);
            newObject = Instantiate(seeThroughWallPrefab, targetPos, Quaternion.identity);
            newObject.transform.parent = seeThroughWalls;
        }


        for (int z = 0; z < zSize * 2; z++)
        {
            targetPos = new Vector3(xSize * prefabSize - 5f, 0, z * (prefabSize * 0.5f) + 2.5f);
            newObject = Instantiate(seeThroughWallPrefab, targetPos, Quaternion.identity);
            newObject.transform.localEulerAngles = new Vector3(0, 90, 0);
            newObject.transform.parent = seeThroughWalls;
        }

    }
}

using JetBrains.Annotations;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GenerationTwo : MonoBehaviour
{
    int xSize = 10;
    int ySize = 10;

    public GameObject wallPrefab;
    public GameObject floorPrefab;

    public int floorSize = 5;
    private void Awake()
    {
        //   GenerateRoom(new Vector2(-2, -2), new Vector2(8, 8));
        //  GenerateTunnelOne(new Vector2(4, 4), new Vector2(-4,8));
        //   GenerateTunnelOne(new Vector2(1,2), new Vector2(5,5));
        //  GenerateRoom(new Vector2(5,6), new Vector2(7,7));   
        // GenerateRoom(new Vector2(3, 5), new Vector2(2, 2));
        //   GenerateTunnelOne(new Vector2(-1,-1), new Vector2(3, 3));

        //    Vector2 size = new Vector2(5, 5);
        //   Vector2 start = new Vector2(5, 5);
        //  GenerateRoom(size, start);
        //  start = new Vector2(15, 15); ;
        // GenerateRoom(size, start);
        // GenerateTunnelOne(new Vector2(10, 10), new Vector2(8, 10));

        // GenerateTunnelTwo(new Vector2(0, 0), new Vector2(-5, 5));

     //   GenerateTunnelOne(Vector2.zero, new Vector2(5,-5));  
    }

    void GenerateRoom(Vector2 start, Vector2 size)
    {
        int xSize = (int)size.x;
        int ySize = (int)size.y;

        int xStart = (int)start.x;
        int yStart = (int)start.y;

        int xEnd = xStart + xSize;
        int yEnd = yStart + ySize;

        Vector3 calculate;

        for (int x = xStart; x < xEnd; x++)
        {
            for (int y = yStart; y < yEnd; y++)
            {
                calculate = new Vector3(x * floorSize, 0, y * floorSize);
                GameObject newObject = Instantiate(floorPrefab, calculate, Quaternion.identity);
                newObject.transform.name = x + "," + y;
            }
        }
    }

    void GenerateMainLocation(Vector2 start)
    {
        for(int x = (int)start.x; x < (int)start.x + 6; x++)
        {
            InstantiateFloor(new Vector2(x,start.y));
        }

        for(int y = (int)start.y; y < (int) start.y + 6; y++)
        {
            InstantiateFloor(new Vector2(start.x, y));
        }
    }

    void GenerateTunnelOne(Vector2 start, Vector2 end)
    {
        int xLength = (int)end.x - (int)start.x;
        int yLength = (int)end.y - (int)start.y;
        int yTarget = (int)start.y + (yLength / 2);


        Vector3 calculate = new Vector3(start.x * floorSize, 0, start.y * floorSize);
        GameObject newObject = Instantiate(floorPrefab, calculate, Quaternion.identity);
        newObject.transform.name = start.x.ToString() + " " + start.y.ToString();


        if (yLength > 0)
        {
            while (start.y != yTarget)
            {
                start.y++;

                calculate = new Vector3(start.x * floorSize, 0, start.y * floorSize);
                newObject =  Instantiate(floorPrefab, calculate, Quaternion.identity);
                newObject.transform.name = start.x.ToString() + " " + start.y.ToString();
            }
        }
        else if (yLength < 0)
        {
            while (start.y != yTarget)
            {
                start.y--;

                calculate = new Vector3(start.x * floorSize, 0, start.y * floorSize);
                newObject =  Instantiate(floorPrefab, calculate, Quaternion.identity);
                newObject.transform.name = start.x.ToString() + " " + start.y.ToString();
            }
        }

        if(xLength > 0)
        {
            while(start.x != end.x)
            {
                start.x++;

                calculate = new Vector3(start.x * floorSize, 0, start.y * floorSize);
                newObject =  Instantiate(floorPrefab, calculate, Quaternion.identity);
                newObject.transform.name = start.x.ToString() + " " + start.y.ToString();
            }
        }
        else if(xLength < 0)
        {
            while (start.x != end.x)
            {
                start.x--;

                calculate = new Vector3(start.x * floorSize, 0, start.y * floorSize);
                newObject =  Instantiate(floorPrefab, calculate, Quaternion.identity);
                newObject.transform.name = start.x.ToString() + " " + start.y.ToString();
            }
        }

        if (yLength > 0)
        {
            while (start.y != end.y)
            {
                start.y++;

                calculate = new Vector3(start.x * floorSize, 0, start.y * floorSize);
                newObject =  Instantiate(floorPrefab, calculate, Quaternion.identity);
                newObject.transform.name = start.x.ToString() + " " + start.y.ToString();
            }
        }
        else if (yLength < 0)
        {
            while (start.y != end.y)
            {
                start.y--;

                calculate = new Vector3(start.x * floorSize, 0, start.y * floorSize);
                newObject = Instantiate(floorPrefab, calculate, Quaternion.identity);
                newObject.transform.name = start.x.ToString() + " " + start.y.ToString();
            }
        }
    }

    void GenerateTunnelTwo(Vector2 start, Vector2 end)
    {
        int xLength = (int)end.x - (int)start.x;
        int yLength = (int)end.y - (int)start.y;
        int xTarget = (int)start.x + (xLength / 2);


        Vector3 calculate = new Vector3(start.x * floorSize, 0, start.y * floorSize);
        GameObject newObject = Instantiate(floorPrefab, calculate, Quaternion.identity);
        newObject.transform.name = start.x.ToString() + " " + start.y.ToString();

        if (xLength > 0)
        {
            while (start.x != xTarget)
            {
                start.x++;

                calculate = new Vector3(start.x * floorSize, 0, start.y * floorSize);
                newObject = Instantiate(floorPrefab, calculate, Quaternion.identity);
                newObject.transform.name = start.x.ToString() + " " + start.y.ToString();
            }
        }
        else if (xLength < 0)
        {
            while (start.x != xTarget)
            {
                start.x--;

                calculate = new Vector3(start.x * floorSize, 0, start.y * floorSize);
                newObject = Instantiate(floorPrefab, calculate, Quaternion.identity);
                newObject.transform.name = start.x.ToString() + " " + start.y.ToString();
            }
        }



        if (yLength > 0)
        {
            while (start.y != end.y)
            {
                start.y++;

                calculate = new Vector3(start.x * floorSize, 0, start.y * floorSize);
                newObject = Instantiate(floorPrefab, calculate, Quaternion.identity);
                newObject.transform.name = start.x.ToString() + " " + start.y.ToString();
            }
        }
        else if (yLength < 0)
        {
            while (start.y != end.y)
            {
                start.y--;

                calculate = new Vector3(start.x * floorSize, 0, start.y * floorSize);
                newObject = Instantiate(floorPrefab, calculate, Quaternion.identity);
                newObject.transform.name = start.x.ToString() + " " + start.y.ToString();
            }
        }



        if (xLength > 0)
        {
            while (start.x != end.x)
            {
                start.x++;

                calculate = new Vector3(start.x * floorSize, 0, start.y * floorSize);
                newObject = Instantiate(floorPrefab, calculate, Quaternion.identity);
                newObject.transform.name = start.x.ToString() + " " + start.y.ToString();
            }
        }
        else if (xLength < 0)
        {
            while (start.x != end.x)
            {
                start.x--;

                calculate = new Vector3(start.x * floorSize, 0, start.y * floorSize);
                newObject = Instantiate(floorPrefab, calculate, Quaternion.identity);
                newObject.transform.name = start.x.ToString() + " " + start.y.ToString();
            }
        }


    }

    void InstantiateFloor(Vector2 targetLocation)
    {
        Vector3 calculate = new Vector3(targetLocation.x * floorSize, 0, targetLocation.y * floorSize);
        GameObject newObject = Instantiate(floorPrefab, calculate, Quaternion.identity);
        newObject.transform.name = targetLocation.x + "," + targetLocation.y;
    }

    


}

/*
 *         //  for (int z = 0; z < zLength /2; z++)
//  {
//    targetZ = zStart + z;
//   targetX = xStart;
//   calculate = new Vector3(xStart * floorSize, 0, targetZ * floorSize);
//  GameObject newObject = Instantiate(floorPrefab,calculate, Quaternion.identity);
//   newObject.transform.name = xStart + "," + z;

//   }

//  while(start != end)
//  {
bool stepOneComplete = false;
int counter = 0;
//     bool zBigger = 

while (!stepOneComplete)
{
    calculate = new Vector3(start.x * floorSize, 0, start.y * floorSize);
    GameObject newObject = Instantiate(floorPrefab, calculate, Quaternion.identity);
    newObject.transform.name = start.ToString();
    start.y++;
    counter++;
    if (counter >= zLength / 2)
    {
        stepOneComplete = true;
    }
}
}
}
*/












// Start is called before the first frame update
//  void Start()
//  {

//  }

// Update is called once per frame
//   void Update()
//  {

//  }


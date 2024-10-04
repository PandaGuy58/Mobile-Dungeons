using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chamber
{
   // bool active;
    float activeValue = 0f;

    float activeValueChange = 0.2f;
    public AnimationCurve curve;
    List<LightNoise> lightNoise;
    List<MeshRenderer> renderers;

  //  Vector2 generationStartPoint;

    public void Initialise(AnimationCurve curve, List<LightNoise> lightNoise)
    {
        this.curve = curve;
        this.lightNoise = lightNoise;
    }


    public void ActivateChamber()
    {
        activeValue += activeValueChange * Time.deltaTime;
        if (activeValue > 1)
        {
            activeValue = 1;
        }

        float curveEvaluate = curve.Evaluate(activeValue);
        UpdateComponents(curveEvaluate);
    }

    public void DeactivateChamber()
    {
        activeValue -= activeValueChange * Time.deltaTime;
        if(activeValue < 0)
        {
            activeValue = 0;
        }

        float curveEvaluate = curve.Evaluate(activeValue);
        UpdateComponents(curveEvaluate);

    }

    void UpdateComponents(float curveEvaluate)
    {

    }

}

public class Contents
{
    public MeshRenderer column;
  //  public GameObject floor;

    public MeshRenderer xWall;
    public MeshRenderer[] xCutWalls;
    public float xWallTransition;

    public MeshRenderer yWall;
    public MeshRenderer[] yCutWalls;
    public float yWallTransition;

    public void UpdateXWall(float value)
    {

    }
}




//void SetAlphaOfMaterial(MeshRenderer rend, float value)

// Start is called before the first frame update
//void Start()
// {

// }

// Update is called once per frame
//   void Update()
//  {

// }

//  public void Activate(bool active)
// {
//     this.active = active;
// }
/*

 public void UpdateChamber()
 {
     if(active)
     {
         activeValue += activeValueChange * Time.deltaTime;
         if(activeValue > 1)
         {
             activeValue = 1;
         }
     }
     else
     {
         activeValue -= activeValueChange * Time.deltaTime; 
     }

     float curveEvaluate = curve.Evaluate(activeValue);
 }
}
*/

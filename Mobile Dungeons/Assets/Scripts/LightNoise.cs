using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class LightNoise : MonoBehaviour
{
    Light light;
    public GameObject targetObject;

    float intensityBase = 20;
    float intensityJumpScale = 12;
    float intensityScrollSpeed = 1.5f;

    float positionScrollSpeed = 1;

    Vector3 initialPositionValue;
    float startScrollValue = 0;
    float positionJumpScale = 0.75f;

    // Start is called before the first frame update
    void Awake()
    {
        light = targetObject.GetComponent<Light>();
        startScrollValue = Random.Range(1, 1000000);
        initialPositionValue = targetObject.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        CalculateIntensity();
        CalculatePosition();
    }

    void CalculateIntensity()
    {
        float calculate = (intensityJumpScale * Mathf.PerlinNoise(startScrollValue + Time.time * intensityScrollSpeed, 1f + Time.time * intensityScrollSpeed));
        if (calculate < 0)
        {
            calculate = -calculate;
        }
    //    float intensityEvaluate = curve.Evaluate(activateValue);

        calculate = intensityBase + calculate;
       // calculate *= intensityEvaluate;
        light.intensity = calculate;
    }

    void CalculatePosition()
    {
        float x = Mathf.PerlinNoise(startScrollValue + Time.time * positionScrollSpeed, 1 + Time.time * positionScrollSpeed) - 0.5f;
        float y = Mathf.PerlinNoise(startScrollValue + Time.time * positionScrollSpeed, 1 + Time.time * positionScrollSpeed) - 0.5f;
        float z = Mathf.PerlinNoise(startScrollValue + 4 + Time.time * positionScrollSpeed, 5 + Time.time * positionScrollSpeed) - 0.5f;

        Vector3 calculatePostion = initialPositionValue;
        calculatePostion.x += x * positionJumpScale;
        calculatePostion.y += y * positionJumpScale;
        calculatePostion.z += z * positionJumpScale;


        light.transform.position = calculatePostion;
    }
}






    /*
    void CalculateIntensity()
    {
        float calculate = (intensityJumpScale * Mathf.PerlinNoise(Time.time * intensityScrollSpeed, 1f + Time.time * intensityScrollSpeed));
        if (calculate < 0)
        {
            calculate = -calculate;
        }
        calculate = intensityBase + calculate;
        light.intensity = calculate;
    }
}
    */

/*
 * public class LightNoise : MonoBehaviour
{
    // Start is called before the first frame update
 //   public float perlinNoiseValue;

    public float intensityBase = 0.5f;
    public float intensityJumpScale = 0.1f;
    public float intensityScrollSpeed = 1;

    public Light compLight;
    public GameObject lightObject;

    public float positionScrollSpeed = 1;
    public float positionJumpScale = 0.25f;

    Vector3 initialPositionValue;

  //  float timeLightEnabled = -1;
  //  Camera cam;

  //  float curveEvaluate = 0;
    public AnimationCurve curve;

    float timeActivate = 0;
    float activateValue = 0;

    float startScrollValue = 0;
    // Update is called once per frame

    private void Start()
    {
        initialPositionValue = lightObject.transform.localPosition;
        startScrollValue = Random.Range(1, 1000000);
    }

    void Update()
    {
        CalculateIntensity();
        CalculatePosition();
    //    CalculatePosition();

        if(timeActivate > Time.time)
        {
            activateValue += Time.deltaTime;
            if(activateValue > 1)
            {
                activateValue = 1;
            }
        }
        else
        {
            activateValue -= Time.deltaTime;
            if(activateValue < 0)
            {
                activateValue = 0;
            }
        }

        if(activateValue == 0)
        {
            lightObject.SetActive(false);
        }
        else
        {
            lightObject.SetActive(true);
        }
    }

    public void Activate()
    {
        timeActivate = Time.time + 0.1f;

    }



    void CalculateIntensity()
    {
        float calculate = (intensityJumpScale * Mathf.PerlinNoise(startScrollValue + Time.time * intensityScrollSpeed, 1f + Time.time * intensityScrollSpeed));
        if (calculate < 0)
        {
            calculate = -calculate;
        }
        float intensityEvaluate = curve.Evaluate(activateValue);

        calculate = intensityBase + calculate;
        calculate *= intensityEvaluate;
        compLight.intensity = calculate;
    }

    void CalculatePosition()
    {
        float x = Mathf.PerlinNoise(startScrollValue + Time.time * positionScrollSpeed, 1 + Time.time * positionScrollSpeed) - 0.5f;
        float y = Mathf.PerlinNoise(startScrollValue + Time.time * positionScrollSpeed, 1 + Time.time * positionScrollSpeed) - 0.5f;
        float z = Mathf.PerlinNoise(startScrollValue + 4 + Time.time * positionScrollSpeed, 5 + Time.time * positionScrollSpeed) - 0.5f;

        Vector3 calculatePostion = initialPositionValue;
        calculatePostion.x += x * positionJumpScale;
        calculatePostion.y += y * positionJumpScale;
        calculatePostion.z += z * positionJumpScale;


        lightObject.transform.localPosition = calculatePostion;
    }
}
*/
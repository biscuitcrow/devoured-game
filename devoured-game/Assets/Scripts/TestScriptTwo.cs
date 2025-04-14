using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScriptTwo : MonoBehaviour
{
     
    Vector3 PositionOfOOnQuadraticBezierCurve(Vector3 pointX, Vector3 pointY, Vector3 pointM, float t)
    {
        //Calculating the position using quadratic bezier equation 
        Vector3 currentPosition = (pointX * (1-t) * (1-t)) + (pointM * 2 * (1-t) * t) + (pointY * t * t);
        return (currentPosition);
    } 

}

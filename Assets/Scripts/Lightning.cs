using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Lightning : MonoBehaviour
{

    public Material lightningMat;

    LineRenderer lineRend;
    public Vector3 endPoint;
    public Vector3 midPoint;
    public float radius;
    public int segments;
    public Color color;
    public float bezierTimer;


    // Use this for initialization
    void Start()
    {

    }

    public void ready()
    {
        lineRend = GetComponent<LineRenderer>();
        lineRend.material = lightningMat;
        lineRend.positionCount = segments;
        int closest = (int)(bezierTimer * segments);
        for (int i = 0; i < segments; i++)
        {
            Vector3 pos = Vector3.zero;
            if (i != 0 || i != segments - 1)
            {
                pos = new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), Random.Range(-radius, radius));
            }
            pos += (Bezier(transform.position, midPoint, endPoint, ((float)i) / ((float)segments)));
            //if (i == closest)
            //{
            //    pos += new Vector3(Random.Range(-radius * 2, radius * 2), Random.Range(-radius * 2, radius * 2), Random.Range(-radius * 2, radius * 2));
            //}
            //if (i + 1 == closest || i - 1 == closest)
            //{
            //    pos += new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), Random.Range(-radius, radius));
            //}
            lineRend.SetPosition(i, pos);
        }
    }

    // Update is called once per frame
    void Update()
    {
        color.a -= 10.0f;

        lineRend.endColor = color;
        lineRend.startColor = color;

        if (color.a <= 0.0f)
        {
            DestroyImmediate(this.gameObject);
        }

    }
    static public Vector3 Bezier(Vector3 _initPoint, Vector3 _midPoint, Vector3 _endPoint, float _time)
    {
        Vector3 t_bezierTime = new Vector3();
        t_bezierTime.x = Mathf.Pow(1 - _time, 2) * _initPoint.x + (1 - _time) * 2 * _time * _midPoint.x + _time * _time * _endPoint.x;
        t_bezierTime.y = Mathf.Pow(1 - _time, 2) * _initPoint.y + (1 - _time) * 2 * _time * _midPoint.y + _time * _time * _endPoint.y;
        t_bezierTime.z = Mathf.Pow(1 - _time, 2) * _initPoint.z + (1 - _time) * 2 * _time * _midPoint.z + _time * _time * _endPoint.z;
        return t_bezierTime;
    }
}


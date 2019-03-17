using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusIndicator : MonoBehaviour
{
    public LineRenderer LineRenderer;

    public int Points;
    public float Radius;

    private void Awake()
    {
        RefreshIndicator();
    }

    public void SetIndicatorVisible(bool visible = true)
    {
        LineRenderer.enabled = visible;
    }

    private Vector3[] GetPoints()
    {
        Vector3[] points = new Vector3[Points];

        for (int i = 0; i < Points; i++)
        {
            var point = new Vector3(Mathf.Cos((Mathf.PI * 2f / Points) * i) * Radius, 0, Mathf.Sin((Mathf.PI * 2f / Points) * i) * Radius);
            point = point + transform.position;
            points[i] = point;
        }

        return points;
    }

    public void RefreshIndicator()
    {
        var points = GetPoints();

        LineRenderer.positionCount = points.Length;
        LineRenderer.SetPositions(points);
    }
}

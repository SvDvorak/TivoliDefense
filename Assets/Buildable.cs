using System;
using System.Collections;
using UnityEngine;

public class Buildable : MonoBehaviour
{
    public GameObject Preview;
    public GameObject Built;

    private float _height;
    private Vector3 _startPosition;
    private Vector3 _endPosition;

    public void Start()
    {
    }

    public void StartBuilding()
    {
        Built.SetActive(true);
        _height = Built.GetComponent<MeshRenderer>().bounds.size.y;
        _endPosition = Built.transform.position;
        _startPosition = Built.transform.position - Vector3.up*_height;
        Built.transform.position = _startPosition;
        StartCoroutine(FinishBuilding());
    }

    private IEnumerator FinishBuilding()
    {
        yield return CoroutineActions.Interpolate(4, GrowFromGround);
        Preview.SetActive(false);
        Built.GetComponent<ActivateCollidable>().Activate();
    }

    private void GrowFromGround(float value)
    {
        Built.transform.position = Vector3.Lerp(_startPosition, _endPosition, value);
    }

    public void Update()
    {
        
    }

}

public static class CoroutineActions
{
    public static IEnumerator Interpolate(float runTime, Action<float> interpolationAction)
    {
        var startTime = Time.time;
        var elapsed = 0f;

        while (elapsed < 1)
        {
            elapsed = Mathf.Clamp((Time.time - startTime) / runTime, 0, 1);
            interpolationAction(elapsed);
            yield return 0;
        }
    }
}

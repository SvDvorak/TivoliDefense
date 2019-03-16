using System.Collections;
using UnityEngine;

public class Buildable : MonoBehaviour
{
    public GameObject Preview;
    public GameObject Built;
    public bool IsFinished;

    private float _height;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private BuiltObject _builtObject;

    public void Start()
    {
        _builtObject = Built.GetComponent<BuiltObject>();
        _builtObject.Destroyed += BuiltDestroyed;
        if(IsFinished)
            Finish();
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
        Finish();
    }

    private void GrowFromGround(float value)
    {
        Built.transform.position = Vector3.Lerp(_startPosition, _endPosition, value);
    }

    private void Finish()
    {
        Built.SetActive(true);
        Preview.SetActive(false);
        _builtObject.Activate();
    }

    private void BuiltDestroyed()
    {
        Debug.Log("DESTROYED!");
        _builtObject.Destroyed -= BuiltDestroyed;
    }
}
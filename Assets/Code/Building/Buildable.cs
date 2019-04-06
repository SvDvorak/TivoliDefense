using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildable : MonoBehaviour
{
    public GameObject Preview;
    public GameObject BuildRadiusIndicator;
    public GameObject Built;
    public BuildingIndicator BuildingIndicator;
    public float BuildRadius;
    public float BuildProgress = 0f;

    private List<Survivor> BuildingSurvivors = new List<Survivor>();

    private float _height;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private BuiltObject _builtObject;
    private SphereCollider _buildTrigger;
    private RadiusIndicator _buildRadiusIndicator;

    public void Awake()
    {

        _buildTrigger = GetComponent<SphereCollider>();
        _buildTrigger.radius = BuildRadius;

        _buildRadiusIndicator = BuildRadiusIndicator.GetComponent<RadiusIndicator>();
        _buildRadiusIndicator.SetIndicatorVisible(true);

        _builtObject = Built.GetComponent<BuiltObject>();
        _builtObject.Destroyed += BuiltDestroyed;

     
    }

  

    public void Update()
    {
        if (BuildProgress < 1f)
        {
            var builderCount = BuildingSurvivors.Count;

            BuildingIndicator.BuildSpeed = builderCount;

            BuildProgress += builderCount * 0.2f * Time.deltaTime;

            Built.transform.localPosition = Vector3.Lerp(_startPosition, _endPosition, BuildProgress);
        }
        else
        {
            Finish();
        }
    }

    public void EnableConstruction()
    {
        _buildTrigger.enabled = true;

        _height = Built.GetComponent<SkinnedMeshRenderer>().bounds.size.y;
        _endPosition = Built.transform.localPosition;
        _startPosition = Built.transform.localPosition - Vector3.up * _height;
        Built.transform.localPosition = _startPosition;

        _builtObject.Activate();
        Built.SetActive(true);

        BuildingIndicator.gameObject.SetActive(true);

        _buildRadiusIndicator.Radius = BuildRadius;
        _buildRadiusIndicator.Points = 30;
        _buildRadiusIndicator.RefreshIndicator();
    }

    private void Finish()
    {
        var buildRadiusIndicator = BuildRadiusIndicator.GetComponent<RadiusIndicator>();
        buildRadiusIndicator.SetIndicatorVisible(false);
        Preview.SetActive(false);
        BuildingIndicator.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Survivor"))
        {
            var survivor = other.gameObject.GetComponentInParent<Survivor>();
            BuildingSurvivors.Add(survivor);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Survivor"))
        {
            var survivor = other.gameObject.GetComponentInParent<Survivor>();
            BuildingSurvivors.Remove(survivor);
        }
    }

    private void BuiltDestroyed()
    {
        _builtObject.Destroyed -= BuiltDestroyed;
    }
}
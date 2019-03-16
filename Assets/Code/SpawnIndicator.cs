using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIndicator : MonoBehaviour
{
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        var heightOffset = Mathf.Sin(Time.time * 3);
        transform.position = _startPosition + new Vector3(0, heightOffset, 0);
    }
}

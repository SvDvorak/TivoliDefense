using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionIndicator : MonoBehaviour
{
    private Vector3 _startLocalPosition;


    // Start is called before the first frame update
    void Start()
    {
        _startLocalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        var offset = Mathf.Sin(Time.time * 3);
        transform.localPosition = _startLocalPosition + new Vector3(0, offset, 0);
    }
}

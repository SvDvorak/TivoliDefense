using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingIndicator : MonoBehaviour
{
    public float BuildSpeed;

    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Sin(Time.time * BuildSpeed * 2) * 45f));
    }
}

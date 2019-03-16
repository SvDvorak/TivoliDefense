using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Gamestate
{
    public static Vector3 InputGroundPosition;
}

public class Build : MonoBehaviour
{
    public GameObject WallPrefab;
    private GameObject _currentlyPlacing;
    private Vector3 _placementStart;

    public void Start()
    {
        
    }

    public void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var isInputOverGround = Physics.Raycast(ray, out var hit, Mathf.Infinity, LayerMask.GetMask("Ground"));
        Gamestate.InputGroundPosition = hit.point;
        if (!isInputOverGround)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            _currentlyPlacing = Instantiate(WallPrefab, hit.point + new Vector3(0, 2, 0), Quaternion.identity);
            _placementStart = hit.point;
        }

        if (_currentlyPlacing != null)
        {
            var inputDelta = hit.point - _placementStart;
            if(inputDelta.sqrMagnitude > 0)
            {
                var look = Quaternion.LookRotation(inputDelta, Vector3.up);
                _currentlyPlacing.transform.rotation = look;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _currentlyPlacing.GetComponent<Buildable>().StartBuilding();
                _currentlyPlacing = null;
            }
        }
    }
}

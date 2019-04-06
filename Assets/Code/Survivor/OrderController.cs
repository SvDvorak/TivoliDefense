using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderController : MonoBehaviour
{

    public OrderMode Mode = OrderMode.MoveSelect;

    public List<Survivor> SelectedSurvivors = new List<Survivor>();
    public Image SelectionBoxImage;

    private Vector2 _dragStart = Vector2.zero;
    private Vector2 _dragEnd = Vector2.zero;
    private Bounds _selectionBounds = new Bounds();

    public GameObject WallPlacementIndicator;
    public float WallLength;
    public GameObject WallPrefab;

    private bool _isDragging = false;
    private GameObject _currentWallPlacementIndicator;
    private Vector3 _placementStart;
    private Vector3 _placementEnd;

    void Update()
    {
        //Debug.Log($"Mode: {Mode}");
        switch (Mode)
        {
            case OrderMode.MoveSelect:
                {
                    HandleMoveSelect();
                    break;
                }
            case OrderMode.BuildWall:
                {
                    HandleWallBuilding();
                    break;

                }
            case OrderMode.BuildStructure:
                {
                    break;
                }
        }
    }

    private void HandleMoveSelect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isDragging = true;
            _dragStart = Input.mousePosition;
            SelectionBoxImage.rectTransform.sizeDelta = Vector2.zero;
            SelectionBoxImage.enabled = true;
        }

        if (Input.GetMouseButton(0) && _isDragging)
        {
            _dragEnd = Input.mousePosition;

            var midPoint = (_dragStart + _dragEnd) / 2f;

            _selectionBounds.center = midPoint;
            _selectionBounds.size = new Vector3(Mathf.Abs(_dragStart.x - _dragEnd.x), Mathf.Abs(_dragStart.y - _dragEnd.y));

            SelectionBoxImage.rectTransform.anchoredPosition = midPoint;
            SelectionBoxImage.rectTransform.sizeDelta = _selectionBounds.size;
        }

        if (Input.GetMouseButtonUp(0) && _isDragging)
        {
            _dragEnd = Input.mousePosition;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100, Color.yellow, 5);
            var isInputOverGround = Physics.Raycast(ray, out var hit, Mathf.Infinity, LayerMask.GetMask("Ground", "Survivor"));
            Gamestate.InputGroundPosition = hit.point;


            if (Vector2.Distance(_dragStart, _dragEnd) <= Mathf.Epsilon)
            {
                if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Survivor"))
                {
                    var survivor = hit.collider.gameObject.transform.root.GetComponent<Survivor>();

                    if (Input.GetKey(KeyCode.LeftControl) == false)
                    {
                        DeselectAll();
                        AddToSelection(survivor);
                    }
                    else
                    {
                        ToggleFromSelection(survivor);
                    }

                    Debug.Log("Hit survivor!");
                }
                else
                {
                    if (Input.GetKey(KeyCode.LeftControl) == false)
                        DeselectAll();
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftControl) == false)
                    DeselectAll();

                var survivors = FindObjectsOfType<Survivor>();

                foreach (var survivor in survivors)
                {
                    var survivorScreenPos = Camera.main.WorldToScreenPoint(survivor.transform.position);
                    survivorScreenPos.z = 0f;

                    if (_selectionBounds.Contains(survivorScreenPos))
                    {
                        AddToSelection(survivor);
                    }

                }
            }


            _isDragging = false;
            _dragEnd = Input.mousePosition;
            SelectionBoxImage.enabled = false;
        }

        if (Input.GetMouseButton(1) && _isDragging == false)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var isInputOverGround = Physics.Raycast(ray, out var hit, Mathf.Infinity, LayerMask.GetMask("Ground"));
            Gamestate.InputGroundPosition = hit.point;

            if (hit.collider == null) return;

            foreach (var survivor in SelectedSurvivors)
            {
                var navAgent = survivor.NavMeshAgent.SetDestination(hit.point);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && _isDragging == false)
        {
            DeselectAll();
        }


        if (Input.GetKeyDown(KeyCode.Alpha1) && _isDragging == false)
        {
            Mode = OrderMode.BuildWall;
        }
    }

    private void HandleWallBuilding()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var isInputOverGround = Physics.Raycast(ray, out var hit, Mathf.Infinity, LayerMask.GetMask("Ground"));
        Gamestate.InputGroundPosition = hit.point;

        if (Input.GetMouseButtonDown(0))
        {
            _placementStart = hit.point;
            _currentWallPlacementIndicator = Instantiate(WallPlacementIndicator, _placementStart, Quaternion.identity);
        }

        if (_currentWallPlacementIndicator != null)
        {
            var inputDelta = hit.point - _placementStart;
            var dragLength = inputDelta.magnitude;
            var dragDirection = inputDelta.normalized;

            int wallSections = Mathf.RoundToInt(dragLength / WallLength);
            var wallLookDirection = Quaternion.LookRotation(inputDelta, Vector3.up);

            _placementEnd = hit.point;
            if (inputDelta.sqrMagnitude > 0)
            {
                _currentWallPlacementIndicator.transform.rotation = wallLookDirection;
                _currentWallPlacementIndicator.transform.localScale = new Vector3(1, 1, wallSections * WallLength);
                _currentWallPlacementIndicator.transform.position = _placementStart + dragDirection * (wallSections * WallLength / 2);
            }

            if (Input.GetMouseButtonUp(0))
            {

                Destroy(_currentWallPlacementIndicator);

                List<GameObject> newWallSections = new List<GameObject>();

                for (int i = 0; i < wallSections; i++)
                {
                    var newWallSection = Instantiate(WallPrefab, null);
                    newWallSection.transform.position = _placementStart + dragDirection * ((WallLength / 2) + WallLength * i);
                    newWallSection.transform.rotation = wallLookDirection;

                    var buildable = newWallSection.GetComponent<Buildable>();
                    buildable.EnableConstruction();

                    newWallSections.Add(newWallSection);
                }

                //_currentlyPlacing.GetComponent<Buildable>().StartBuilding();
                foreach (var survivor in SelectedSurvivors)
                {
                    //survivor.NavMeshAgent.SetDestination(_currentlyPlacing.transform.position);
                    //var buildable = _currentlyPlacing.GetComponent<Buildable>();
                    //buildable.EnableConstruction();
                }
                _currentWallPlacementIndicator = null;

                Mode = OrderMode.MoveSelect;
            }
        }

        if (_currentWallPlacementIndicator == null && Input.GetKeyDown(KeyCode.Escape))
        {
            Mode = OrderMode.MoveSelect;
        }
    }

    private void ToggleFromSelection(Survivor survivor)
    {
        if (SelectedSurvivors.Contains(survivor))
            RemoveFromSelection(survivor);
        else
            AddToSelection(survivor);
    }

    private void AddToSelection(Survivor survivor)
    {
        survivor.SetSelectionIndicatorActive(true);
        SelectedSurvivors.Add(survivor);
    }

    private void RemoveFromSelection(Survivor survivor)
    {
        survivor.SetSelectionIndicatorActive(false);
        SelectedSurvivors.Remove(survivor);
    }

    private void DeselectAll()
    {
        foreach (var survivor in SelectedSurvivors)
        {
            survivor.SetSelectionIndicatorActive(false);
        }

        SelectedSurvivors.Clear();
    }


}

public enum OrderMode
{
    MoveSelect,
    BuildWall,
    BuildStructure
}

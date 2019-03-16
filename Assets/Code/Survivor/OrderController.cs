using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderController : MonoBehaviour
{

    public List<Survivor> SelectedSurvivors = new List<Survivor>();
    public Image SelectionBoxImage;

    private Vector2 DragStart = Vector2.zero;
    private Vector2 DragEnd = Vector2.zero;
    private Bounds SelectionBounds = new Bounds();


    private bool _isDragging = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            _isDragging = true;
            DragStart = Input.mousePosition;
            SelectionBoxImage.rectTransform.sizeDelta = Vector2.zero;
            SelectionBoxImage.enabled = true;
        }

        if (Input.GetMouseButton(0) && _isDragging)
        {
            DragEnd = Input.mousePosition;

            var midPoint = (DragStart + DragEnd) / 2f;

            SelectionBounds.center = midPoint;
            SelectionBounds.size = new Vector3(Mathf.Abs(DragStart.x - DragEnd.x), Mathf.Abs(DragStart.y - DragEnd.y));

            SelectionBoxImage.rectTransform.anchoredPosition = midPoint;
            SelectionBoxImage.rectTransform.sizeDelta = SelectionBounds.size;
        }

        if (Input.GetMouseButtonUp(0) && _isDragging)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var isInputOverGround = Physics.Raycast(ray, out var hit, Mathf.Infinity, LayerMask.GetMask("Ground", "Survivor", "Contraption"));
            Gamestate.InputGroundPosition = hit.point;


            if (Vector2.Distance(DragStart, DragEnd) <= Mathf.Epsilon)
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

                    if (SelectionBounds.Contains(survivorScreenPos))
                    {
                        AddToSelection(survivor);
                    }

                }
            }


            _isDragging = false;
            DragEnd = Input.mousePosition;
            SelectionBoxImage.enabled = false;
        }



        if (Input.GetMouseButtonDown(1))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var isInputOverGround = Physics.Raycast(ray, out var hit, Mathf.Infinity, LayerMask.GetMask("Ground"));
            Gamestate.InputGroundPosition = hit.point;

            foreach (var survivor in SelectedSurvivors)
            {
                survivor.NavMeshAgent.SetDestination(hit.point);
            }
        }
    }

    private void ToggleFromSelection(Survivor survivor)
    {
        if (SelectedSurvivors.Contains(survivor))
        {
            RemoveFromSelection(survivor);
        }
        else
        {
            AddToSelection(survivor);
        }
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

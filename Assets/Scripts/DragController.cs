using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    public Draggable LastDragged => _lastDragged;

    private bool _isDragActive = false;

    private Vector2 _screenPosition;

    private Draggable _lastDragged;

    private Vector3 _worldPosition;

    void Awake()
    {
        DragController[] controllers = FindObjectsOfType<DragController>();

        if(controllers.Length > 1)
        {
             Destroy(gameObject);
        }

    }
    void Update()
    {
        if(_isDragActive && 
        (Input.GetMouseButtonUp(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)))
        {
            Drop();
            return;
        }

        if(Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            _screenPosition = new Vector2(mousePosition.x, mousePosition.y);
        }
        else
        {
            return;
        }
            
        _worldPosition = Camera.main.ScreenToWorldPoint(_screenPosition);

        if(_isDragActive)
        {
            Drag();
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(_worldPosition, Vector2.zero);

            if(hit.collider != null)
            {
                Draggable draggable = hit.transform.gameObject.GetComponent<Draggable>();

                if(draggable != null)
                {
                    _lastDragged = draggable;
                    StartDrag();
                }
            }
        }
            
    }

    void StartDrag()
    {
        _lastDragged.LastPosition = _lastDragged.transform.position;
        UpdateDragStatus(true);
    }
    void Drag() 
    {
        _lastDragged.transform.position = new Vector2(_worldPosition.x, _worldPosition.y);
    }

    void Drop() 
    {
        UpdateDragStatus(false);
    }

    void UpdateDragStatus(bool isDragging)
    {
        _isDragActive = _lastDragged.IsDragging = isDragging;
        
        if(isDragging)
            _lastDragged.gameObject.layer = Layer.Dragging;
        else
            _lastDragged.gameObject.layer = Layer.Default;
    }
}

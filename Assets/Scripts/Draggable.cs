using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public static int score = 0;
    private const string DropValid = "DropValid";
    private const string DropValid2H = "DropValid2H";
    private const string DropValid2V = "DropValid2V";
    private const string DropValid3H = "DropValid3H";
    private const string DropValid3V = "DropValid3V";
    private const string DropInvalid = "DropInvalid";
    public bool IsDragging;

    public Vector3 LastPosition;

    private Collider2D _collider;

    private DragController _dragController;

    private float _movementTime = 15f;

    private System.Nullable<Vector3> _movementDestination;

    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _dragController = FindObjectOfType<DragController>();
    }

    void FixedUpdate() 
    {
        if(_movementDestination.HasValue)
        {
            if(IsDragging)
            {
                _movementDestination = null;
                return;
            }

            if(transform.position == _movementDestination)
            {
                gameObject.layer = Layer.Default;
                _movementDestination = null;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, _movementDestination.Value, _movementTime * Time.fixedDeltaTime);
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {   
        Draggable dragged = GetComponent<Draggable>();
        Draggable collidedDraggable = other.GetComponent<Draggable>();

        if(collidedDraggable != null  && _dragController.LastDragged.gameObject == gameObject)
        {
            ColliderDistance2D colliderDistance2D = other.Distance(_collider);
            Vector3 diff = new Vector3(colliderDistance2D.normal.x, colliderDistance2D.normal.y) * colliderDistance2D.distance;
            transform.position -= diff;
        }

        if(other.CompareTag(DropValid) && ((System.Math.Abs(dragged._collider.bounds.size.y - other.bounds.size.y) < 0.9) && (System.Math.Abs(dragged._collider.bounds.size.y - other.bounds.size.y) < 0.9)))
        {
            _movementDestination = other.transform.position;
            score += 1;
            Debug.Log(score);
            other.gameObject.tag = DropInvalid;
        }

        else if (other.CompareTag(DropValid2H) && ((System.Math.Abs(dragged._collider.bounds.size.y - other.bounds.size.y) < 0.9) && (System.Math.Abs(dragged._collider.bounds.size.y - other.bounds.size.y) < 0.9)))
        {
            _movementDestination = other.transform.position;
            score += 2;
            Debug.Log(score);
            other.gameObject.tag = DropInvalid;
        }

        else if (other.CompareTag(DropValid2V) && ((System.Math.Abs(dragged._collider.bounds.size.y - other.bounds.size.y) < 0.9) && (System.Math.Abs(dragged._collider.bounds.size.y - other.bounds.size.y) < 0.9)))
        {
            _movementDestination = other.transform.position;
            score += 2;
            Debug.Log(score);
            other.gameObject.tag = DropInvalid;
        }

        else if (other.CompareTag(DropValid3H) && ((System.Math.Abs(dragged._collider.bounds.size.x - other.bounds.size.x) < 0.9) && (System.Math.Abs(dragged._collider.bounds.size.y - other.bounds.size.y) < 0.9)))
        {
            _movementDestination = other.transform.position;
            score += 3;
            Debug.Log(score);
            other.gameObject.tag = DropInvalid;
        }

        else if (other.CompareTag(DropValid3V) && ((System.Math.Abs(dragged._collider.bounds.size.x - other.bounds.size.x) < 0.9) && (System.Math.Abs(dragged._collider.bounds.size.y - other.bounds.size.y) < 0.9)))
        {
            _movementDestination = other.transform.position;
            score += 3;
            Debug.Log(score);
            other.gameObject.tag = DropInvalid;
        }

        else if(other.CompareTag(DropInvalid))
        {
            _movementDestination = LastPosition;
        }

    }

    void OnTriggerExit2D(Collider2D other) {

        Draggable collidedDraggable = other.GetComponent<Draggable>();

        if(collidedDraggable != null)
        {

            if(other.CompareTag(DropInvalid) && (other.bounds.size.x < 2 && other.bounds.size.y < 2))
            {
                score -= 1;
                other.gameObject.tag = DropValid;
            }

            else if(other.CompareTag(DropInvalid) && (other.bounds.size.x > 2 && other.bounds.size.y < 2))
            {
                score -= 2;
                other.gameObject.tag = DropValid2H;
            }

            else if(other.CompareTag(DropInvalid) && (other.bounds.size.x < 2 && other.bounds.size.y > 2))
            {
                score -= 2;
                other.gameObject.tag = DropValid2V;
            }

            else if(other.CompareTag(DropInvalid) && (other.bounds.size.x > 4 && other.bounds.size.y < 2))
            {
                score -= 3;
                other.gameObject.tag = DropValid3H;
            }

            else if(other.CompareTag(DropInvalid) && (other.bounds.size.x < 2 && other.bounds.size.y > 4))
            {
                score -= 3;
                other.gameObject.tag = DropValid3V;
            }
        }
    }
}

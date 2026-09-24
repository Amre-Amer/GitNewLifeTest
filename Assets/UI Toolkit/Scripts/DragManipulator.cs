using UnityEngine;
using UnityEngine.UIElements;

public class DragManipulator : PointerManipulator
{
    private Vector2 _targetStartPosition;
    private Vector3 _pointerStartPosition;
    private bool _enabled;

    public DragManipulator(VisualElement target)
    {
        this.target = target;
    }

    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<PointerDownEvent>(OnPointerDown);
        target.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        target.RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<PointerDownEvent>(OnPointerDown);
        target.UnregisterCallback<PointerMoveEvent>(OnPointerMove);
        target.UnregisterCallback<PointerUpEvent>(OnPointerUp);
    }

    private void OnPointerDown(PointerDownEvent evt)
    {
        if (_enabled) return;

        // Cache positions to calculate correct delta offset
        _targetStartPosition = new Vector2(target.layout.x, target.layout.y);
        _pointerStartPosition = evt.position;

        // Capture pointer to track dragging even if mouse leaves the element bounds
        target.CapturePointer(evt.pointerId);
        _enabled = true;
        evt.StopPropagation();
    }

    private void OnPointerMove(PointerMoveEvent evt)
    {
        if (!_enabled || !target.HasPointerCapture(evt.pointerId)) return;

        // Calculate how far the mouse has moved
        Vector3 delta = evt.position - _pointerStartPosition;

        // Apply updated position directly to styles (using absolute positioning)
        target.style.left = _targetStartPosition.x + delta.x;
        target.style.top = _targetStartPosition.y + delta.y;
        
        evt.StopPropagation();
    }

    private void OnPointerUp(PointerUpEvent evt)
    {
        if (!_enabled || !target.HasPointerCapture(evt.pointerId)) return;

        // Release the mouse pointer capture
        target.ReleasePointer(evt.pointerId);
        _enabled = false;
        evt.StopPropagation();
    }
}

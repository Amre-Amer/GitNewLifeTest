using UnityEngine;
using UnityEngine.UIElements;

public class RotateManipulator : PointerManipulator
{
    private bool m_Active;
    private float m_StartAngle;
    private float m_ElementInitialRotation;

    public RotateManipulator()
    {
        m_Active = false;
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
        if (m_Active) return;

        // Calculate center of target in local panel coordinates
        Vector2 center = target.layout.center;
        Vector2 pointerPos = target.WorldToLocal(evt.position);

        // Track the starting angle of the drag gesture
        m_StartAngle = Mathf.Atan2(pointerPos.y - center.y, pointerPos.x - center.x) * Mathf.Rad2Deg;
        
        // Grab current rotation angle (default to 0 if not set)
        m_ElementInitialRotation = target.style.rotate.value.angle.value;

        target.CapturePointer(evt.pointerId);
        m_Active = true;
        evt.StopPropagation();
    }

    private void OnPointerMove(PointerMoveEvent evt)
    {
        if (!m_Active || !target.HasPointerCapture(evt.pointerId)) return;

        Vector2 center = target.layout.center;
        Vector2 pointerPos = target.WorldToLocal(evt.position);

        // Calculate current angle and delta change
        float currentAngle = Mathf.Atan2(pointerPos.y - center.y, pointerPos.x - center.x) * Mathf.Rad2Deg;
        float deltaAngle = currentAngle - m_StartAngle;

        // Apply new rotation to the UI Toolkit element style
        target.style.rotate = new Rotate(new Angle(m_ElementInitialRotation + deltaAngle, AngleUnit.Degree));
        evt.StopPropagation();
    }

    private void OnPointerUp(PointerUpEvent evt)
    {
        if (!m_Active || !target.HasPointerCapture(evt.pointerId)) return;

        target.ReleasePointer(evt.pointerId);
        m_Active = false;
        evt.StopPropagation();
    }
}

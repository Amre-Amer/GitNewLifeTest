using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;

        // Find your visual element by name or class
        VisualElement rotatableElement = root.Q<VisualElement>("map");

        if (rotatableElement != null)
        {
            // Ensure transform-origin is centered for clean rotation
            rotatableElement.style.transformOrigin = new TransformOrigin(Length.Percent(50), Length.Percent(50));
            
            // Add the custom rotate manipulator
            rotatableElement.AddManipulator(new RotateManipulator());
        }
    }
}
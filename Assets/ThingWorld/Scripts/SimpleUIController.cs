using UnityEngine;
using UnityEngine.UIElements; // Required namespace for UI Toolkit

public class SimpleUIController : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button myButton;
    private Label myLabel;

    private void OnEnable()
    {
        // 1. Get the UIDocument component attached to this GameObject
        uiDocument = GetComponent<UIDocument>();

        // 2. Get the root visual element of the UI hierarchy
        VisualElement root = uiDocument.rootVisualElement;

        // 3. Find the Button element by its name (defined in UXML)
        myButton = root.Q<Button>("MyButton");

        // 4. Register a callback function for the click event
        if (myButton != null)
        {
            myButton.clicked += OnButtonClicked;
        }

        myLabel = root.Q<Label>("MyLabel");
        myLabel.text = "length of tail";

        // 1. Create or reference your data source
        // ThingsSO data = ScriptableObject.CreateInstance<ThingsSO>();

        // // 2. Bind the Label's "text" property to the source variable
        // myLabel.SetBinding("text", new DataBinding()
        // {
        //     dataSource = data,
        //     dataSourcePath = new Unity.Properties.PropertyPath(nameof(ThingsSO.Health))
        // });
    }

    public void SetLabel(string txt)
    {
        myLabel.text = txt;
    }

// private void OnEnable()
//     {
//         var r = GetComponent<UIDocument>();
//         VisualElement ve = new VisualElement();
//         Color color = new(0, 1, 0, .5f);
//         ve.style.backgroundColor = new StyleColor(color);
//         ve.StretchToParentSize();
//         r.rootVisualElement.Add(ve);
//     }

    private void OnDisable()
    {
        // Always unregister callbacks when disabling to prevent memory leaks
        if (myButton != null)
        {
            myButton.clicked -= OnButtonClicked;
        }
    }

    private void OnButtonClicked()
    {
        Debug.Log("Button was clicked!");
    }
}

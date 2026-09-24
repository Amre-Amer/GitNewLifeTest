using System;
using UnityEngine;
// You must include the UIElements namespace to work with UI Toolkit
using UnityEngine.UIElements; 
using Unity.Properties;

public class MainMenuController : MonoBehaviour
{
    public CamMgr camMgr;
    private UIDocument _uiDocument;
    private Button _myButtonRotate;
    private Button _myButtonToggle2D3D;
    private Button _myButtonZoom;
    private Button _myButtonCamA;
    private Button _myButtonCamB;
    private Button _myButtonCamC;
    private Button _myButtonMain;
    private Button _myButtonSplit;
    private Button _myButtonReset;
    private bool yn3D;
    public GameObject map;
    private float targetYawDelta = 15f;
    private float targetYaw;
    private Quaternion quatOrig;
    public float rotationSpeed = 450f;
    private bool ynRotate;
    private bool ynZoom;
    public TimerMgr timerMgr;

    private void Start()
    {
        quatOrig = map.transform.rotation;
        ynRotate = true;
        targetYaw = map.transform.eulerAngles.y;
        Debug.Log("Rotate");
    }

    private void OnEnable()
    {
        _uiDocument = GetComponent<UIDocument>();
        VisualElement root = _uiDocument.rootVisualElement;

        var healthLabel = root.Q<Label>("HealthLabel");
        healthLabel.dataSource = timerMgr;
        DataBinding binding = new DataBinding
        {
            dataSourcePath = new PropertyPath(nameof(timerMgr.currentHealth)),
            bindingMode = BindingMode.ToTarget // Updates UI when source changes
        };

        // 3. Bind the 'text' property of the Label element to the rule
        healthLabel.SetBinding(nameof(Label.text), binding);
        
        _myButtonRotate = root.Q<Button>("Rotate");
        _myButtonToggle2D3D = root.Q<Button>("Toggle2D3D");
        _myButtonZoom = root.Q<Button>("Zoom");
        _myButtonCamA = root.Q<Button>("CamA");
        _myButtonCamB = root.Q<Button>("CamB");
        _myButtonCamC = root.Q<Button>("CamC");
        _myButtonSplit = root.Q<Button>("Split");
        _myButtonMain = root.Q<Button>("Main");
        _myButtonReset = root.Q<Button>("Reset");
        if (_myButtonRotate != null) _myButtonRotate.clicked += OnButtonClickedRotate;
        if (_myButtonToggle2D3D != null) _myButtonToggle2D3D.clicked += OnButtonClickedToggle2D3D;
        if (_myButtonZoom != null) _myButtonZoom.clicked += OnButtonClickedZoom;
        if (_myButtonCamA != null) _myButtonCamA.clicked += OnButtonClickedCamA;
        if (_myButtonCamB != null) _myButtonCamB.clicked += OnButtonClickedCamB;
        if (_myButtonCamC != null) _myButtonCamC.clicked += OnButtonClickedCamC;
        if (_myButtonSplit != null) _myButtonSplit.clicked += OnButtonClickedSplit;
        if (_myButtonMain != null) _myButtonMain.clicked += OnButtonClickedMain;
        if (_myButtonReset != null) _myButtonReset.clicked += OnButtonClickedReset;
    }

    private void OnDisable()
    {
        if (_myButtonRotate != null) _myButtonRotate.clicked -= OnButtonClickedRotate;
        if (_myButtonToggle2D3D != null) _myButtonToggle2D3D.clicked -= OnButtonClickedToggle2D3D;
        if (_myButtonZoom != null) _myButtonZoom.clicked -= OnButtonClickedZoom;
        if (_myButtonCamA != null) _myButtonCamA.clicked -= OnButtonClickedCamA;
        if (_myButtonCamB != null) _myButtonCamB.clicked -= OnButtonClickedCamB;
        if (_myButtonCamC != null) _myButtonCamC.clicked -= OnButtonClickedCamC;
        if (_myButtonSplit != null) _myButtonSplit.clicked -= OnButtonClickedSplit;
        if (_myButtonMain != null) _myButtonMain.clicked -= OnButtonClickedMain;
        if (_myButtonReset != null) _myButtonReset.clicked -= OnButtonClickedReset;
    }

    private void OnButtonClickedRotate()
    {
        ynRotate = true;
        targetYaw = map.transform.eulerAngles.y + targetYawDelta;
        Debug.Log("Rotate");
    }

    private void OnButtonClickedToggle2D3D()
    {
        yn3D = !yn3D;
        if (yn3D)
        {
            camMgr.SetCam3D();
        }
        else
        {
            camMgr.SetCam2D();
        }
        Debug.Log("Toggle2D3D");
    }
    
    private void OnButtonClickedZoom()
    {
        ynZoom = !ynZoom;
        if (ynZoom)
        {
            camMgr.ZoomIn();
        }
        else
        {
            camMgr.ZoomOut();
        }
        Debug.Log("Zoom");
    }
    
    private void OnButtonClickedCamA()
    {
        camMgr.SetCam(CamType.A);
        Debug.Log("CamA");
    }
    
    private void OnButtonClickedCamB()
    {
        camMgr.SetCam(CamType.B);
        Debug.Log("CamB");
    }
    
    private void OnButtonClickedCamC()
    {
        camMgr.SetCam(CamType.C);
        Debug.Log("CamC");
    }
    
    private void OnButtonClickedSplit()
    {
        camMgr.SetCam(CamType.Split);
        Debug.Log("Split");
    }
    
    private void OnButtonClickedMain()
    {
        camMgr.SetCam(CamType.Main);
        Debug.Log("Main");
    }
    
    private void OnButtonClickedReset()
    {
        camMgr.SetCam(CamType.Reset);
        map.transform.rotation = quatOrig;
        Debug.Log("Reset");
    }
    
    void Update()
    {
        if (ynRotate)
        {
            float currentYaw = Mathf.SmoothDampAngle(map.transform.eulerAngles.y, targetYaw, ref rotationSpeed, 0.1f);
            map.transform.rotation = Quaternion.Euler(map.transform.eulerAngles.x, currentYaw, map.transform.eulerAngles.z);
            if (Mathf.Abs(map.transform.eulerAngles.y - targetYaw) < 0.01f)
            {
                ynRotate = false;
            }
        }
    }
}

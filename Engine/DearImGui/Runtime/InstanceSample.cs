using UnityEngine;

public class InstanceSample : MonoBehaviour
{
    [SerializeField]
    private UImGui.UImGui _uimGuiInstance;

    private void Awake()
    {
        if (_uimGuiInstance == null)
        {
            Debug.LogError("Must assign a UImGuiInstance or use UImGuiUtility with Do Global Events on UImGui component.");
        }

        _uimGuiInstance.Layout += OnLayout;
        _uimGuiInstance.OnInitialize += OnInitialize;
        _uimGuiInstance.OnDeinitialize += OnDeinitialize;
    }

    private void OnLayout(UImGui.UImGui obj)
    {
        // Unity Update method. 
        // Your code belongs here! Like ImGui.Begin... etc.
    }

    private void OnInitialize(UImGui.UImGui obj)
    {
        // runs after UImGui.OnEnable();
    }

    private void OnDeinitialize(UImGui.UImGui obj)
    {
        // runs after UImGui.OnDisable();
    }

    private void OnDisable()
    {
        _uimGuiInstance.Layout -= OnLayout;
        _uimGuiInstance.OnInitialize -= OnInitialize;
        _uimGuiInstance.OnDeinitialize -= OnDeinitialize;
    }
}
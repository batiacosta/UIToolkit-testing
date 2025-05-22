using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    private VisualElement _bottomContainer;
    private Button _openButton;
    private Button _closeButton;

    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        if (root == null) return;
        _bottomContainer = root.Q<VisualElement>("BottomContainer");
        _openButton = root.Q<Button>("OpenButton");
        _closeButton = root.Q<Button>("CloseButton");
        
        _bottomContainer.style.display = DisplayStyle.None;
        _openButton.clicked += OpenButtonClicked;
        _closeButton.clicked += CloseButtonClicked;
    }
    private void OnDestroy()
    {
        _openButton.clicked -= OpenButtonClicked;
    }

    private void CloseButtonClicked()
    {
        _bottomContainer.style.display = DisplayStyle.None;
    }

    private void OpenButtonClicked()
    {
        _bottomContainer.style.display = DisplayStyle.Flex;
    }
}

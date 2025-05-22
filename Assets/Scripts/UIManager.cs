using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    private VisualElement _bottomContainer;
    private Button _openButton;
    private Button _closeButton;
    private VisualElement _bottomSheet;
    private VisualElement _scrim;

    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        if (root == null) return;
        _bottomContainer = root.Q<VisualElement>("BottomContainer");
        _openButton = root.Q<Button>("OpenButton");
        _closeButton = root.Q<Button>("CloseButton");
        _bottomSheet = root.Q<VisualElement>("BottomSheet");
        _scrim = root.Q<VisualElement>("Scrim");
        
        _bottomContainer.style.display = DisplayStyle.None;
        _openButton.clicked += OpenButtonClicked;
        _closeButton.clicked += CloseButtonClicked;
    }
    private void OnDestroy()
    {
        _openButton.clicked -= OpenButtonClicked;
    }
    private void OpenButtonClicked()
    {
        _bottomContainer.style.display = DisplayStyle.Flex;
        _bottomSheet.AddToClassList("bottomSheet-up");
        _scrim.AddToClassList("scrim-fadeIn");
    }
    private void CloseButtonClicked()
    {
        _bottomSheet.RemoveFromClassList("bottomSheet-up");
        _scrim.RemoveFromClassList("scrim-fadeIn");
        _bottomContainer.style.display = DisplayStyle.None;
    }
}

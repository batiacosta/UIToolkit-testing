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
    private VisualElement _astronaut;

    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        if (root == null) return;
        _bottomContainer = root.Q<VisualElement>("BottomContainer");
        _openButton = root.Q<Button>("OpenButton");
        _closeButton = root.Q<Button>("CloseButton");
        _bottomSheet = root.Q<VisualElement>("BottomSheet");
        _scrim = root.Q<VisualElement>("Scrim");
        _astronaut = root.Q<VisualElement>("Astronaut");
        
        _bottomContainer.style.display = DisplayStyle.None;
        _openButton.clicked += OpenButtonClicked;
        _closeButton.clicked += CloseButtonClicked;

        Invoke("AnimateAstroaut", 0.1f); // Since the first frame does not assign a state in UI, we need to wait a bit before playing the initial animation
    }

    private void AnimateAstroaut()
    {
        // Debug.Log($"{_astronaut.ClassListContains("astronaut-OutScreen")}");
        _astronaut.RemoveFromClassList("astronaut-OutScreen");
    }

    private void OnDestroy()
    {
        _openButton.clicked -= OpenButtonClicked;
        _closeButton.clicked -= CloseButtonClicked;
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

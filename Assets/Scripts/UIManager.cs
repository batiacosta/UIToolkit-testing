using System;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    private VisualElement _bottomContainer;
    private Button _openButton;
    private Button _closeButton;
    private VisualElement _bottomSheet;
    private VisualElement _scrim;
    private VisualElement _astronaut;
    private VisualElement _lady;
    private Label _typedLabel;

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
        _lady = root.Q<VisualElement>("LadyImage");
        _typedLabel = root.Q<Label>("TypedMessage");
        
        _bottomContainer.style.display = DisplayStyle.None;
        _openButton.clicked += OpenButtonClicked;
        _closeButton.clicked += CloseButtonClicked;

        Invoke("AnimateAstroaut", 0.1f); // Since the first frame does not assign a state in UI, we need to wait a bit before playing the initial animation
    }

    private void AnimateAstroaut()
    {
        // _astronaut.ClassListContains("astronaut-OutScreen") -> checks whether or not the class exists in the viual element
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

        AnimateLadyImage();
        AnimateTypingLabel();
    }

    private void AnimateTypingLabel()
    {
        string message = "Hola buenas, buenas, esto es un typed text";
        _typedLabel.text = string.Empty;
        DOTween.To(() => _typedLabel.text, x => _typedLabel.text = x, message, 2f).SetEase(Ease.Linear);
    }

    private void AnimateLadyImage()
    {
        _lady.ToggleInClassList("lady-up");
        _lady.RegisterCallback<TransitionEndEvent>((_)=> AnimateLadyImage());
    }

    private void CloseButtonClicked()
    {
        _bottomSheet.RemoveFromClassList("bottomSheet-up");
        _scrim.RemoveFromClassList("scrim-fadeIn");
        _bottomContainer.style.display = DisplayStyle.None;
    }
}

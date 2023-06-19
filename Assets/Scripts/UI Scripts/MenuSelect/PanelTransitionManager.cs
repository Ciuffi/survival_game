using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PanelTransitionManager : MonoBehaviour
{
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private float transitionDuration = 0.5f;
    [SerializeField] private float initialXValue;  // set this value according to your UI layout
    [SerializeField] private float targetXValue;  // set this value according to your UI layout

    [SerializeField] private float initialYValue;  // set this value according to your UI layout
    [SerializeField] private float targetYValue;  // set this value according to your UI layout

    [SerializeField] private bool moveVertically; // The transition direction (false for horizontal, true for vertical)
    [SerializeField] private bool moveToLeft; // The direction of movement

    [SerializeField] private Button transitionButton; // Reference to the button
    [SerializeField] private Sprite initialButtonSprite; // The initial sprite of the button
    [SerializeField] private Sprite transitionedButtonSprite; // The sprite of the button when the panel is transitioned

    private int sortingLayerOffset = 0; // Offset to manage the sorting layer order
    private bool isTransitioning = false;
    public bool isTransitioned = false; // New bool to track the state of the panel
    public Canvas canvas;

    private void Start()
    {
        // Position the panel initially off the screen to the specified side
        Vector2 initialPosition = contentPanel.anchoredPosition;
        if (moveVertically)
        {
            initialPosition.y = initialYValue;
        }
        else
        {
            initialPosition.x = initialXValue;
        }
        contentPanel.anchoredPosition = initialPosition;

        // Set the initial button sprite
        transitionButton.image.sprite = initialButtonSprite;
        canvas = contentPanel.GetComponentInParent<Canvas>();
    }

    public void OnButtonClick()
    {  
        // Change the button sprite immediately when the button is clicked
        isTransitioned = !isTransitioned;
        transitionButton.image.sprite = isTransitioned ? transitionedButtonSprite : initialButtonSprite;

        float targetPos;

        // Check the panel's current position and determine where to move next
        if (moveVertically)
        {
            targetPos = contentPanel.anchoredPosition.y == targetYValue ? initialYValue : targetYValue;
        }
        else
        {
            targetPos = contentPanel.anchoredPosition.x == targetXValue ? initialXValue : targetXValue;
        }

        // Set the sorting layer immediately when the button is clicked
        if (!isTransitioned)
        {
            sortingLayerOffset = 0; // Reset the sorting layer offset when closing the panel
        }
        else
        {
            sortingLayerOffset = GetMaxSortingOrder() + 1; // Increment the sorting layer offset to be one more than the maximum sorting order of other panels
        }
        canvas.sortingOrder = sortingLayerOffset;

        // Start the transition
        isTransitioning = true;
        if (moveVertically)
        {
            contentPanel.DOAnchorPosY(targetPos, transitionDuration)
                .SetEase(Ease.OutBack)
                .OnComplete(() => isTransitioning = false);
        }
        else
        {
            contentPanel.DOAnchorPosX(targetPos, transitionDuration)
                .SetEase(Ease.OutBack)
                .OnComplete(() => isTransitioning = false);
        }
    }

    private int GetMaxSortingOrder()
    {
        PanelTransitionManager[] panelManagers = FindObjectsOfType<PanelTransitionManager>();
        int maxSortingOrder = int.MinValue;
        foreach (PanelTransitionManager manager in panelManagers)
        {
            int sortingOrder = manager.canvas.sortingOrder;
            maxSortingOrder = Mathf.Max(maxSortingOrder, sortingOrder);
        }
        return maxSortingOrder;
    }
}
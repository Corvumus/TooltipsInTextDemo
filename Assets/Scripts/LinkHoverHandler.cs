using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class LinkHoverHandler : MonoBehaviour
{
    private TextMeshProUGUI mainTextMesh;
    private Canvas canvas;
    private Camera camera;
    private RectTransform mainRectTransform;

    private int currentLink;

    public UnityAction<string> OnEnteredLink;
    public UnityAction OnExitedLink;

    private void Awake()
    {
        mainTextMesh = GetComponentInChildren<TextMeshProUGUI>();
        canvas = GetComponentInParent<Canvas>();
        mainRectTransform = GetComponent<RectTransform>();
        
        if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
            camera = Camera.main;
    }

    private void CheckLink()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        
        bool isIntersectingRectTransform = TMP_TextUtilities.IsIntersectingRectTransform(mainRectTransform, mousePosition, camera);

        if (!isIntersectingRectTransform)
        {
            if (currentLink != -1)
            {
                OnExitedLink?.Invoke();
                currentLink = -1;
            }
            return;
        }

        int intersectingLink = TMP_TextUtilities.FindIntersectingLink(mainTextMesh, mousePosition, camera);

        if (currentLink != intersectingLink)
            OnExitedLink?.Invoke();

        currentLink = intersectingLink;

        if (intersectingLink == -1)
            return;

        TMP_LinkInfo linkInfo = mainTextMesh.textInfo.linkInfo[intersectingLink];
            
        OnEnteredLink?.Invoke(linkInfo.GetLinkID());
    }

    private void Update()
    {
        CheckLink();
    }
}
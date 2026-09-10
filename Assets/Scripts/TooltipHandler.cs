using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipHandler : MonoBehaviour
{
    [SerializeField] private Dictionary<string, string> tooltipDict;
    [SerializeField] private GameObject tooltipContainer;

    private LinkHoverHandler hoverHandler; 
    private TextMeshProUGUI tooltipTextMesh;

    private void Awake()
    {
        tooltipTextMesh = tooltipContainer.GetComponentInChildren<TextMeshProUGUI>();
        hoverHandler = FindAnyObjectByType<LinkHoverHandler>();
    }

    private void OnEnable()
    {
        hoverHandler.OnEnteredLink += ShowTooltip;
        hoverHandler.OnExitedLink += CloseTooltip;
    }

    private void OnDisable()
    {
        hoverHandler.OnEnteredLink -= ShowTooltip;
        hoverHandler.OnExitedLink -= CloseTooltip;
    }

    private void ShowTooltip(string id)
    {
        if (tooltipDict.TryGetValue(id, out string text))
        {
            tooltipContainer.SetActive(true);
            tooltipTextMesh.text = text;
        }
    }

    public void CloseTooltip()
    {
        tooltipContainer.SetActive(false);
    }
}

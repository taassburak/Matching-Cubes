using UnityEngine;
using Scripts.Managers;
public class UIPanel : CustomBehaviour
{
    public UIManager UIManager { get; set; }

    public LayerNames OriginalSortingLayer;

    public virtual void Initialize(UIManager uiManager)
    {
        UIManager = uiManager;
        GameManager = UIManager.GameManager;

        if (Canvas != null)
            Canvas.sortingOrder = (int)OriginalSortingLayer;
    }

    public virtual void ShowPanel()
    {
        gameObject.SetActive(true);
    }

    public virtual void HidePanel()
    {
        gameObject.SetActive(false);
    }

    #region Layer Sorting

    public virtual void SetSortingLayersOfChildren(int layer)
    {
        Component[] meshRenderers = GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer child in meshRenderers)
            child.sortingOrder = layer;

        Component[] canvasses = GetComponentsInChildren<Canvas>();

        foreach (Canvas child in meshRenderers)
            child.sortingOrder = layer;
    }

    public virtual void SetSortingLayer(LayerNames layer, bool affectChildren = false)
    {
        Canvas.sortingOrder = (int)layer;

        if (affectChildren)
        {
            SetSortingLayersOfChildren((int)layer);
        }
    }

    public virtual void SetSortingLayer(int layer, bool affectChildren = false)
    {
        Canvas.sortingOrder = layer;

        if (affectChildren)
        {
            SetSortingLayersOfChildren(layer);
        }
    }

    public virtual void ResetSortingLayer(bool affectChildren = false)
    {
        Canvas.sortingOrder = (int)OriginalSortingLayer;

        if (affectChildren)
        {
            SetSortingLayersOfChildren((int)OriginalSortingLayer);
        }
    }

    public virtual void SetSortingLayerAbove(UIPanel panel, bool affectChildren = false)
    {
        Canvas.sortingOrder = panel.Canvas.sortingOrder + 1;

        if (affectChildren)
        {
            SetSortingLayersOfChildren(Canvas.sortingOrder);
        }
    }

    public virtual void SetSortingLayerBelow(UIPanel panel, bool affectChildren = false)
    {
        Canvas.sortingOrder = panel.Canvas.sortingOrder - 1;

        if (affectChildren)
        {
            SetSortingLayersOfChildren(Canvas.sortingOrder);
        }
    }

    public virtual void SetSortingLayerAbove(MeshRenderer mesh, bool affectChildren = false)
    {
        Canvas.sortingOrder = mesh.sortingOrder + 1;

        if (affectChildren)
        {
            SetSortingLayersOfChildren(Canvas.sortingOrder);
        }
    }
    public virtual void SetSortingLayerBelow(MeshRenderer mesh, bool affectChildren = false)
    {
        Canvas.sortingOrder = mesh.sortingOrder - 1;

        if (affectChildren)
        {
            SetSortingLayersOfChildren(Canvas.sortingOrder);
        }
    }

    #endregion
}
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using VG;

public class SellArea : MonoBehaviour
{
    [SerializeField] private InteractionHandler _interactionHandler;
    [SerializeField] private SellParticle _sellParticlePrefab;
    [SerializeField] private Color _noHighlightColor;
    [SerializeField] private Color _highlightColor;
    [SerializeField] private List<SpriteRenderer> _highligthableSprites;


    private void OnEnable()
    {
        _interactionHandler.onPointerEnter += OnPointerEnter;
        _interactionHandler.onPointerExit += OnPointerExit;
        _interactionHandler.onDrop += OnDrop;
    }

    

    private void OnDisable()
    {
        _interactionHandler.onPointerEnter -= OnPointerEnter;
        _interactionHandler.onPointerExit -= OnPointerExit;
        _interactionHandler.onDrop -= OnDrop;
    }

    private void OnPointerEnter(PointerEventData eventData)
    {
        if (ItemMoveHandler.DraggableItem == null) return;

        foreach (var spriteRenderer in _highligthableSprites)
            spriteRenderer.color = _highlightColor;

    }

    private void OnPointerExit(PointerEventData eventData)
    {
        foreach (var spriteRenderer in _highligthableSprites)
            spriteRenderer.color = _noHighlightColor;
    }

    private void OnDrop(PointerEventData eventData)
    {
        if (ItemMoveHandler.DraggableItem != null)
            SellItem();

        foreach (var spriteRenderer in _highligthableSprites)
            spriteRenderer.color = _noHighlightColor;
    }


    private void SellItem()
    {
        var item = ItemMoveHandler.DraggableItem;
        int sellPrice = TotalRules.GetSellPrice(item.RarityType);

        Vector2 sellPosition = item.transform.position;

        ItemList.PlacedItems.Remove(item);
        ItemList.UpdateItems();

        Destroy(item.gameObject);
        Instantiate(_sellParticlePrefab, sellPosition, Quaternion.identity)
            .SetSellPrice(sellPrice);



        Saves.Int[Key_Save.gems].Value += sellPrice;
    }



}

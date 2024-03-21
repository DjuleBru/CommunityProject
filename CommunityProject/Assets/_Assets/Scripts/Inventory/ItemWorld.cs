using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItemWorld(Vector3 position,Item item) {
        Transform transform = Instantiate(ItemAssets.Instance.itemWorldPrefab, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    private Item item;
    private SpriteRenderer spriteRenderer;
    private Light2D light2D;
    private TextMeshPro textMeshPro;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        light2D = GetComponent<Light2D>();
        textMeshPro = GetComponentInChildren<TextMeshPro>();
    }

    public void SetItem(Item item) {
        this.item = item;
        spriteRenderer.sprite = ItemAssets.Instance.GetItemSO(item.itemType).itemSprite;
        light2D.color = ItemAssets.Instance.GetItemColor(item.itemType);

        if(item.amount > 1) {
            textMeshPro.text = item.amount.ToString();
        } else {
            textMeshPro.text = "";
        }
    }

    public Item GetItem() {
        return item;
    }

    public void DestroySelf() {
        Destroy(gameObject);
    }
}

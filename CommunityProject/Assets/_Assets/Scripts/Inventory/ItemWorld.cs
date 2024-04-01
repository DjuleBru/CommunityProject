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

    public static ItemWorld DropItem(Vector3 dropPosition, Item item, bool droppedByPlayer) {
        Vector3 randomDir = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), 0).normalized;
        float dropForce = 5f;
        float dropDistance = .5f;

        ItemWorld itemWorld = SpawnItemWorld(dropPosition + randomDir * dropDistance, item);

        itemWorld.GetComponent<Rigidbody2D>().AddForce(randomDir * dropForce, ForceMode2D.Impulse);
        return itemWorld;
    }

    private Item item;
    private SpriteRenderer spriteRenderer;
    private Light2D light2D;
    private TextMeshPro textMeshPro;

    private bool droppedByPlayer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        light2D = GetComponent<Light2D>();
        textMeshPro = GetComponentInChildren<TextMeshPro>();

        GetComponent<Collider2D>().enabled = false;

        if (droppedByPlayer) {
            GetComponent<Collider2D>().isTrigger = true;
        } else {
            GetComponent<Collider2D>().isTrigger = false;
            StartCoroutine(MakeColliderTriggerAfterDelay(.5f));
        }

        StartCoroutine(MakeColliderActiveAfterDelay(.5f));
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

    public void SetDroppedByPlayer(bool droppedByPlayer) {
        this.droppedByPlayer = droppedByPlayer;
    }

    private IEnumerator MakeColliderActiveAfterDelay(float delay) {
        yield return new WaitForSeconds(delay);
        GetComponent<Collider2D>().enabled = true;
    }

    private IEnumerator MakeColliderTriggerAfterDelay(float delay) {
        yield return new WaitForSeconds(delay);
        GetComponent<Collider2D>().isTrigger = true;
    }

    public Item GetItem() {
        return item;
    }

    public void DestroySelf() {
        Destroy(gameObject);
    }
}

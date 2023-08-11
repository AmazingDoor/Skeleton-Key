using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Text countText;
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public int count = 1;
    public Image image;
    public Item item;

    public void initItem(Item item)
    {
        Debug.Log("added item");
        this.item = item;
        image.sprite = item.sprite;
        //gameObject.AddComponent<ItemActionHandler>();
        if (item.large) {
            GetComponent<RectTransform>().SetPositionAndRotation(new Vector3(transform.position.x + item.x_offset, transform.position.y, transform.position.z), Quaternion.Euler(1, 1, 1));
            GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);
        }
        switch(item.item_type)
        {
            case ItemType.Lock:
                LockItem lock_item = gameObject.AddComponent<LockItem>();
                lock_item.item = item;
                break;
            case ItemType.Default: break;
            case ItemType.Weapon: break;
            case ItemType.Tool: break;
        }
        
    }

    public void initItem(WeaponItem item)
    {
        Debug.Log("added weapon");
        this.item = item;
        image.sprite = item.sprite;
        //gameObject.AddComponent<ItemActionHandler>();
        if (item.large)
        {
            GetComponent<RectTransform>().SetPositionAndRotation(new Vector3(transform.position.x + item.x_offset, transform.position.y, transform.position.z), Quaternion.Euler(1, 1, 1));
            GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);
        }
        Weapon weapon = gameObject.AddComponent<Weapon>();
        weapon.item = item;
    }

    

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {

        transform.position = new Vector3(Input.mousePosition.x + item.x_offset, Input.mousePosition.y);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        transform.position = new Vector3(parentAfterDrag.position.x + item.x_offset, parentAfterDrag.position.y);
        image.raycastTarget = true;
    }


    public void refreshCount()
    {
        if (this.count <= 0)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            countText.text = count.ToString();
            bool textActive = count > 1;
            countText.gameObject.SetActive(textActive);
        }
    }

}

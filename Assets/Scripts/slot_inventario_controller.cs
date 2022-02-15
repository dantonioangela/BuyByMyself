using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class slot_inventario_controller : MonoBehaviour, IDragHandler, IEndDragHandler
{

    private RectTransform icon;
    private Vector2 initialPos;
    [System.NonSerialized] public bool slotEmpty = true;
    public Product productInThisSlot;
    [SerializeField] private Texture emptyTexture;
    [SerializeField] private Canvas canvas;


    // Start is called before the first frame update
    void Start()
    {
        icon = GetComponent<RectTransform>();
        //transform.GetComponent<RawImage>().texture = emptyTexture;
        initialPos = icon.anchoredPosition;
        //number.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (!slotEmpty)
        {
            if (icon.anchoredPosition.x < 20 || icon.anchoredPosition.x > 450)
            {
                icon.GetComponent<RawImage>().color = new Color(0f, 0f, 0f);
            }
            else icon.GetComponent<RawImage>().color = new Color(1f, 1f, 1f);

            icon.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!slotEmpty)
        {
            if (icon.anchoredPosition.x > 20 && icon.anchoredPosition.x < 450)
            {
                icon.anchoredPosition = initialPos;
            }
            else
            {
                icon.anchoredPosition = initialPos;
                icon.GetComponent<RawImage>().color = new Color(1f, 1f, 1f);
                RemoveProduct();
            }
        }
    }

    public void AddProductInSlot(Product product)
    {
        productInThisSlot = product;
        transform.GetComponent<RawImage>().texture = product.GetComponent<icon>().myIcon;
        slotEmpty = false;
    }

    public void RemoveProduct()
    {
        transform.GetComponent<RawImage>().texture = emptyTexture;
        slotEmpty = true;
        Camera.main.gameObject.transform.parent.GetChild(1).GetComponent<Carrello_controller>().RemoveProductFromChart(productInThisSlot);
        transform.parent.parent.parent.GetComponent<UI_controller>().RemoveProductFromInventario(productInThisSlot);
        productInThisSlot = null;

    }

    public void ReplaceProduct()
    {
        transform.GetComponent<RawImage>().texture = emptyTexture;
        slotEmpty = true;
        productInThisSlot = null;
    }

}

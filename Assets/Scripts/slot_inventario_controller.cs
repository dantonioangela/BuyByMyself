using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class slot_inventario_controller : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Ray ray;
    private RaycastHit hit;
    private RectTransform icon;
    private Vector2 initialPos;
    [System.NonSerialized] public bool slotEmpty = true;
    public Product productInThisSlot;
    [SerializeField] private Texture emptyTexture;
    [SerializeField] private Canvas canvas;
    private ProductLabel productLabel;
    public RawImage ics;
    public RawImage grabbedObject;


    // Start is called before the first frame update
    void Start()
    { 
        productLabel = FindObjectOfType<ProductLabel>();
        icon = GetComponent<RectTransform>();
        icon.position = new Vector3(icon.position.x, icon.position.y, 1f);
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if(!transform.GetComponentInParent<inventario_manager>().isDragging)
        {
            transform.GetComponentInParent<inventario_manager>().isDragging = true;
        }
        grabbedObject.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        grabbedObject.texture = icon.GetComponent<RawImage>().texture;
        if (!slotEmpty)
        {
            productLabel.active = true;
            productLabel.product = productInThisSlot;
            if (icon.anchoredPosition.x < 20 || icon.anchoredPosition.x > 450)
            {
                //icon.GetComponent<RawImage>().color = new Color(0f, 0f, 0f);
                ics.gameObject.SetActive(true);
                ics.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
            else
            {
                icon.GetComponent<RawImage>().color = new Color(1f, 1f, 1f);
                ics.gameObject.SetActive(false);
            }

            //icon.anchoredPosition += eventData.delta / canvas.scaleFactor;
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            icon.position = new Vector3( icon.position.x, icon.position.y, -1f );

        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.GetComponentInParent<inventario_manager>().isDragging = false;
        grabbedObject.texture = emptyTexture;
        icon.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        icon.position = new Vector3(icon.position.x, icon.position.y, 1f);
        ics.gameObject.SetActive(false);
        if (!slotEmpty)
        {
            productLabel.active = false;
            if (icon.anchoredPosition.x > 20 && icon.anchoredPosition.x < 450)
            {
                transform.position = initialPos;
            }
            else
            {
                transform.position = initialPos;
                //icon.GetComponent<RawImage>().color = new Color(1f, 1f, 1f);
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!transform.GetComponentInParent<inventario_manager>().isDragging)
        {
            icon.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!transform.GetComponentInParent<inventario_manager>().isDragging)
        {
            icon.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        }
    }
}

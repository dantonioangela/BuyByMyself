using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ProductLabel : MonoBehaviour
{
    [SerializeField] private GameObject labelUI;
    [SerializeField] private Text productName;
    [SerializeField] private Text productSize;
    [SerializeField] private Text productPrice;
    [SerializeField] private Text productPackaging;
    [SerializeField] private Text productSustainability;
    [SerializeField] private Text productOrigin;
    [SerializeField] private Image expiredLabel;
    [SerializeField] private Image winter;
    [SerializeField] private Image spring;
    [SerializeField] private Image summer;
    [SerializeField] private Image autumn;
    [HideInInspector]
    public Product product = null;
    [HideInInspector]
    public bool active = false;
    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private float speed = 1000f; 
    private float offset = 300f;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = new Vector3(labelUI.transform.position.x,
                                       labelUI.transform.position.y,
                                       labelUI.transform.position.z); 
        targetPosition = new Vector3(labelUI.transform.position.x,
                                     labelUI.transform.position.y - offset,
                                     labelUI.transform.position.z); 
        labelUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            if (product != null && !labelUI.activeSelf)
            {
                labelUI.SetActive(true);

                productName.text = product.model.name;

                productPrice.text = product.model.price.ToString() + " \u20AC";

                productSize.text = product.model.size;

                if (product.model.packaging.HasValue)
                    productPackaging.text = (product.model.packaging.Value == false ? "" : "Imballaggio ecologico");
                else
                    productPackaging.text = "";

                if (product.model.sustainable.HasValue && MenuPrincipale.levelDifficulty == 2)
                    productSustainability.text = (product.model.sustainable.Value == false ? "" : "Produzione sostenibile");
                else
                    productSustainability.text = "";

                if (product.model.origin.HasValue && MenuPrincipale.levelDifficulty >= 1)
                {
                    if (product.model.origin.Value == 1)
                        productOrigin.text = "Prodotto kilometro zero";
                    else if (product.model.origin.Value == 0.5)
                        productOrigin.text = "Prodotto nostrano";
                    else if(product.model.origin.Value == 0)
                        productOrigin.text = "Prodotto estero";
                }
                else
                    productOrigin.text = "";

                if (product.expirated)
                    expiredLabel.enabled = true;
                else
                    expiredLabel.enabled = false;

                if (product.model.season != null && MenuPrincipale.levelDifficulty == 2)
                {
                    winter.enabled = Convert.ToBoolean(product.model.season[3]);
                    spring.enabled = Convert.ToBoolean(product.model.season[0]);
                    summer.enabled = Convert.ToBoolean(product.model.season[1]);
                    autumn.enabled = Convert.ToBoolean(product.model.season[2]);
                }
                else
                {
                    winter.enabled = false;
                    spring.enabled = false;
                    summer.enabled = false;
                    autumn.enabled = false;
                }
            }

            if (labelUI.transform.position.y > targetPosition.y)
                labelUI.transform.Translate(Vector3.down * Time.deltaTime * speed);
        
        }
        else if(!active)
        {
            if(labelUI.transform.position.y < originalPosition.y)
                labelUI.transform.Translate(Vector3.up * Time.deltaTime * speed);
            else
                labelUI.SetActive(false);
        }
    }

}

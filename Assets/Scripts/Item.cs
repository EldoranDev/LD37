using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {

    public float RotationSpeed;

    public GameObject Object;

    public Sprite ItemImage;

    public SpriteRenderer Image;

    public GameObject PickupDialog;

    private bool PickupEnabled
    {
        get
        {
            return PickupDialog.activeInHierarchy;
        }
        set
        {
            PickupDialog.SetActive(value);
        }
    }

	// Use this for initialization
	void Start () {
        Image.sprite = ItemImage;
	}
	
	// Update is called once per frame
	void Update () {
        Image.transform.Rotate(Vector3.forward, -90f * Time.deltaTime);

        if(PickupEnabled)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                var player = GameObject.FindGameObjectWithTag("Player");

                var pick = player.GetComponent<ICanPickUp>();
                
                if(pick != null)
                {
                    pick.Pickup(Object);
                    Destroy(gameObject);
                }
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collider = collision.GetComponent<Collider2D>();

        if (collider != null)
        {
            if(collider.tag == "Player")
            {
                PickupEnabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var collider = collision.GetComponent<Collider2D>();

        if(collider != null)
        {
            if(collider.tag == "Player")
            {
                PickupEnabled = false;
            }
        }
    }
}

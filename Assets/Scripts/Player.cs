using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UnitHealth))]
public class Player : MonoBehaviour, IKillable, ICanPickUp {

    //TODO: Key Mapper (instead of unity one)

    public float Speed;

    public Text AmmoDisplay;

    Rigidbody2D _body;
    HitIndicator _inidicator;

    public Transform Hand;

    Weapon currentWeapon;

	// Use this for initialization
	void Start () {
        _body = GetComponent<Rigidbody2D>();
        _inidicator = GameObject.FindObjectOfType<HitIndicator>();

        var health = GetComponent<UnitHealth>();

        health.OnDamageTaken += () =>
        {
            _inidicator.Hit();
        };
	}
	
	// Update is called once per frame
	void Update () {
        UpdateInput();
	}

    // Update Movement and Player Rotation
    void UpdateInput()
    {
        #region Rotation
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
        #endregion

        #region Movement
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        var velocity = new Vector2(horizontal, vertical);

        _body.MovePosition(_body.position + velocity.normalized * Time.deltaTime * Speed);
        #endregion

        #region Weapon Using
        if(Input.GetMouseButton(0) && currentWeapon != null)
        {
            currentWeapon.Use();
        }
        #endregion

        if(currentWeapon is RangedWeapon)
        {
            AmmoDisplay.text = (currentWeapon as RangedWeapon).Ammonition.ToString();
        }
    }

    public void Kill()
    {
        var world = GameObject.FindObjectOfType<WorldManager>();

        world.RemoveLife();
        Destroy(gameObject);
    }

    public void Pickup(GameObject obj)
    {

        var item = obj.GetComponent<IItem>();
        
        if(item is Weapon)
        {
            var weapon = Instantiate(obj, Hand, false);

            currentWeapon = weapon.GetComponent<Weapon>();
        }
        else
        {
            Debug.Log("Implement logic for: " + item.GetType().ToString());
        }
    }
}

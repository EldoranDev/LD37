using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class Projectile : MonoBehaviour {

    public float Speed;
    public float Lifetime;
    public float Damage;

    public bool Explodes = false;
    public bool AreaDamage;

    private float _timeAlive;

	// Use this for initialization
	void Start () {
        var trail = GetComponent<TrailRenderer>();

        trail.sortingLayerName = "Actor";	
	}

    private void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * Speed, Space.Self);

        _timeAlive += Time.deltaTime;

        if(_timeAlive > Lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            var health = collision.GetComponent<UnitHealth>();
            health.DealDamage(Damage);

            Destroy(gameObject);
        }
    }

}

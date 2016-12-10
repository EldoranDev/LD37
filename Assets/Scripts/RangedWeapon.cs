using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

class RangedWeapon : Weapon
{
    public Projectile Projectile;
    public GameObject Shell;
    public float EjectForce;

    public Transform Muzzle;
    public Transform Eject;

    public int Ammonition;

    protected override bool OnUse()
    {
        var shell = Instantiate(Shell, Eject.position, Quaternion.identity);
        var projectile = Instantiate(Projectile.gameObject, Muzzle.position, transform.rotation).GetComponent<Projectile>();

        Ammonition--;

        projectile.Damage = Damage;

        shell.GetComponent<Rigidbody2D>().AddForce(new Vector2(Eject.up.x, Eject.up.y), ForceMode2D.Impulse);

        if (Ammonition == 0)
        {
            Destroy(gameObject);
        }

        return true;
    }
}

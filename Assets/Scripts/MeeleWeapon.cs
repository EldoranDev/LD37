using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

class MeeleWeapon : Weapon
{
    public float Durability;
    public float DurabilityLoss;
    public float Range;

    protected override bool OnUse()
    {
        if (Durability > 0)
        {

            var hits = Physics2D.OverlapCircleAll(transform.position, Range);

            foreach(var hit in hits)
            {
                var enemy = hit.GetComponent<UnitHealth>();       

                if(enemy != null)
                {
                    if(transform.root.tag != enemy.transform.root.tag)
                    {
                        Durability -= DurabilityLoss;
                        enemy.DealDamage(Damage);
                    }
                }
            }
        }
        else
        {
            Destroy(this);
        }

        return true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}


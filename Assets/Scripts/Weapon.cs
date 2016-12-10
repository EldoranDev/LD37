using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

abstract class Weapon : MonoBehaviour, IItem
{
    public float Cooldown;

    private float CurrentCooldown;

    public float Damage;

    public void Use()
    {
        if(CurrentCooldown >= Cooldown)
        { 
            if(OnUse())
            {
                CurrentCooldown = 0;
            }
        }
    }

    private void Update()
    {
        CurrentCooldown += Time.deltaTime;
    }

    protected abstract bool OnUse();
}

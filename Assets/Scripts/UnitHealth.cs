using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UnitHealth : MonoBehaviour
{

    public float MaxHealth;

    public Color FullHealthColor = Color.green;
    public Color ZeroHealthColor = Color.red;

    public bool HasUI;

    public Slider Slider;
    public Image FillImage;

    private float _health;

    public Action OnDamageTaken;

    private void OnEnable()
    {
        _health = MaxHealth;

        if (HasUI)
        {
            UpdateUI();
        }
    }

    // Update is called once per frame
    void UpdateUI()
    {
        Slider.value = _health;

        FillImage.color = Color.Lerp(ZeroHealthColor, FullHealthColor, _health / MaxHealth);
    }

    public void DealDamage(float dmg)
    {
        _health -= dmg;

        var e = OnDamageTaken;

        if(e != null)
        {
            e();
        }

        if (_health <= 0)
        {
            var unit = GetComponent<IKillable>();
            unit.Kill();
        }
        else
        {
            if (HasUI)
            {
                UpdateUI();
            }
        }
    }
}

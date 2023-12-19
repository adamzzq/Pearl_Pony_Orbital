using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : NetworkBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    //[Command(requiresAuthority = false)]
    //[ClientRpc]
    public void SetHealth(float health)
    {
        if (health > slider.maxValue)
        {
            health = slider.maxValue;
        }
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}

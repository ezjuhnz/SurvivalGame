using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour {

    [SerializeField]
    private Image health_State, energy_State;

	public void Display_HealthState(float hp)
    {
        hp /= 100f;
        health_State.fillAmount = hp;
    }

    public void Display_EnergyState(float energy)
    {
        energy /= 100f;
        energy_State.fillAmount = energy;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetter : MonoBehaviour
{
    public void SetFox()
    {
        PlayerSelector.Instance.playerChosen = "Fox";
    }
    public void SetRabbit()
    {
        PlayerSelector.Instance.playerChosen = "Rabbit";
    }
}

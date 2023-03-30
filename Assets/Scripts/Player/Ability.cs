using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Ability : MonoBehaviour
{
    public float cooldown;
    public Action AbilityActivated;
    public abstract void Activate();
}

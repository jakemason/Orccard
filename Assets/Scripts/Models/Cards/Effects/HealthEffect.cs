﻿using UnityEngine;

[CreateAssetMenu(fileName = "New Health Effect", menuName = "Effects/Health Effect")]
public class HealthEffect : Effect
{
    public int HealthModifier = 1;

    public override void Activate()
    {
        Core.Instance.Health += HealthModifier;
    }

    public void OnValidate()
    {
        InstructionText = HealthModifier > 0 ? $"+{HealthModifier} HP." : $"-{HealthModifier} HP.";
    }
}
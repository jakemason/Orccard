﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance;

    [FormerlySerializedAs("ConstructedTowers")]
    public Dictionary<Vector2, Building> ConstructedBuildings;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        ConstructedBuildings = new Dictionary<Vector2, Building>();
    }

    public static void StartTurn()
    {
        foreach (KeyValuePair<Vector2, Building> tower in Instance.ConstructedBuildings)
        {
            tower.Value.DoStartOfTurnEffects();
        }
    }
}
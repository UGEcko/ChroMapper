﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Code taken from Beat Saber, which provides deltaTime, fixedDeltaTime, and interpolation.
/// </summary>
public class TimeHelper : MonoBehaviour
{
    public static float DeltaTime { get; private set; }
    public static float FixedDeltaTime { get; private set; }
    public static float InterpolationFactor { get; private set; }

    private float accumulator = 0;

    private void Awake()
    {
        FixedDeltaTime = Time.fixedDeltaTime;
        accumulator += FixedDeltaTime;
    }

    private void FixedUpdate()
    {
        FixedDeltaTime = Time.fixedDeltaTime;
        accumulator -= FixedDeltaTime;
    }

    private void Update()
    {
        DeltaTime = Time.deltaTime;
        accumulator += DeltaTime;
        InterpolationFactor = accumulator / FixedDeltaTime;
    }
}

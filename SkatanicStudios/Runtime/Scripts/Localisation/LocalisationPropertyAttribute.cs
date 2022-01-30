using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Exposes a value to be used during reflection for the localisation system
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class LocalisationPropertyAttribute : Attribute
{
    public Type type;

    public string displayName;

    public LocalisationPropertyAttribute(string name, Type t = null)
    {
        type = t;
        displayName = name;
    }
}

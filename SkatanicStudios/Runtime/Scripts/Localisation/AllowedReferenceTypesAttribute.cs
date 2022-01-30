using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class AllowedReferenceTypesAttribute : Attribute
{

    public Type[] types;
    public AllowedReferenceTypesAttribute(params Type[] types)
    {
        this.types = types;
    }
}

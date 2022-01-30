using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Mesh Object")]
public class MeshObject : ScriptableObject
{
    public Mesh mesh;
    public Vector3 scale;
}

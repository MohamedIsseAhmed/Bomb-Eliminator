using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GunScriptableObject/GunType")]
public class GunScriptableObject : ScriptableObject
{
    [SerializeField] private GameObject gun;
}

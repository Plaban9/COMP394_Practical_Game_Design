using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "TooltipAbility")]
public class AbilityInformation : ScriptableObject
{
    [SerializeField] public Sprite icon;
    [SerializeField] public string title;
    [SerializeField] public string description;
}

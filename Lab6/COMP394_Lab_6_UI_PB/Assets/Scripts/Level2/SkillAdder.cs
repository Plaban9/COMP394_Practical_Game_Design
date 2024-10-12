using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAdder : MonoBehaviour
{
    [SerializeField] public GameObject skillButton;

    [SerializeField] public List<AbilityInformation> abilities;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var ability in abilities)
        {
            Instantiate(skillButton, new Vector3(0, 0, 0), Quaternion.identity, transform);
            skillButton.GetComponent<TooltipHoverable>().displayedInfo = ability;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

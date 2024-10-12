using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoRoller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        pelts = 0;
        time = 40f;
        ammoAmount = 6; 
        StartCoroutine(UpdateQuestText());
        StartCoroutine(UpdateHealth());
        StartCoroutine(UpdateWolves());
        StartCoroutine(UpdateAmmo());
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

    }

    [Header("Quest")]
    float time;
    float pelts;
    [SerializeField] public TextMeshProUGUI questText;


    IEnumerator UpdateQuestText()
    {
        while (true)
        {
            var blurb = "<b>Collect wolf pelts</b>\n\nWolves pelts collected: <color=\"red\">{0}/12</color>\nTime left: <color=\"blue\">{1}";

            questText.text = string.Format(blurb, pelts, time);
            yield return new WaitForSeconds(0.1f);
        }
    }

    [Header("Health/Mana")]
    [SerializeField] public Slider hpSlider;
    [SerializeField] public Slider manaSlider;

    IEnumerator UpdateHealth()
    {
        while (true)
        {
            float amount = Mathf.Sin(Mathf.Rad2Deg * time) * 0.25f;
            float amount2 = Mathf.Cos(Mathf.Rad2Deg * time);

            hpSlider.value = amount + 0.5f;
            manaSlider.value = amount2;
            yield return new WaitForSeconds(0.1f);
        }
        
    }

    IEnumerator UpdateWolves()
    {
        while (true)
        {
            pelts++;

            if (pelts >= 12)
            {
                pelts = 0;
                time = 60;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    [Header("Ammo")]
    [SerializeField] public TextMeshProUGUI reloadAnim;
    [SerializeField] public TextMeshProUGUI ammo;
    int ammoAmount;
    int delay;

    IEnumerator UpdateAmmo()
    {
        while (true)
        {
            ammoAmount -= 1;
            reloadAnim.gameObject.SetActive(ammoAmount <= 0);
            ammo.text = $"{ammoAmount}/6";
            if (ammoAmount <= 0)
            {
                yield return new WaitForSeconds(4);
                ammoAmount = 6;
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

}

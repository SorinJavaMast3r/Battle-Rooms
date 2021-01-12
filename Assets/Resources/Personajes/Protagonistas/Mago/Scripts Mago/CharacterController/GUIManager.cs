using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{

    public Slider hpBar;
    public Slider mpBar;

    public Text hpText;
    public Text mpText;

    // Start is called before the first frame update
    void Start()
    {
        PlayerStats playerStats = GetComponent<PlayerStats>();

        this.hpBar.value = (float)playerStats.currentHP / playerStats.maxHP;
        this.mpBar.value = (float)playerStats.currentMP / playerStats.maxMP;

        this.hpText.text = ((float)playerStats.currentHP / playerStats.maxHP * 100).ToString() + "%";
        this.mpText.text = ((float)playerStats.currentMP / playerStats.maxMP * 100).ToString() + "%";
    }

    public void updateHpBar(float currentHP, float maxHP) 
    {
        this.hpBar.value = currentHP / maxHP;
        this.hpText.text = (currentHP / maxHP * 100).ToString() + "%";
    }

    public void updateMpBar(float currentMP, float maxMP)
    {
        this.mpBar.value = currentMP / maxMP;
        this.mpText.text = (currentMP / maxMP * 100).ToString() + "%";
    }
}

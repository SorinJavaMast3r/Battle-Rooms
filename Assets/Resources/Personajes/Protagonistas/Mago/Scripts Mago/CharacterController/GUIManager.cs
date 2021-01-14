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

        this.hpText.text = playerStats.currentHP.ToString() + "/" + playerStats.maxHP;
        this.mpText.text = playerStats.currentMP.ToString() + "/" + playerStats.maxMP;
    }

    public void updateHpBar(float currentHP, float maxHP) 
    {
        this.hpBar.value = currentHP / maxHP;
        this.hpText.text = currentHP.ToString() + "/" + maxHP;
    }

    public void updateMpBar(float currentMP, float maxMP)
    {
        this.mpBar.value = currentMP / maxMP;
        this.mpText.text = currentMP.ToString() + "/" + maxMP;
    }
}

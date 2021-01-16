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

        this.hpBar.value = playerStats.currentHP / playerStats.maxHP;
        this.mpBar.value = playerStats.currentMP / playerStats.maxMP;

        this.hpText.text = Mathf.RoundToInt(playerStats.currentHP).ToString() + "/" + Mathf.RoundToInt(playerStats.maxHP);
        this.mpText.text = Mathf.RoundToInt(playerStats.currentMP).ToString() + "/" + Mathf.RoundToInt(playerStats.maxMP);
    }

    public void updateHpBar(float currentHP, float maxHP) 
    {
        this.hpBar.value = currentHP / maxHP;
        this.hpText.text = Mathf.RoundToInt(currentHP).ToString() + "/" + Mathf.RoundToInt(maxHP);
    }

    public void updateMpBar(float currentMP, float maxMP)
    {
        this.mpBar.value = currentMP / maxMP;
        this.mpText.text = Mathf.RoundToInt(currentMP).ToString() + "/" + Mathf.RoundToInt(maxMP);
    }
}

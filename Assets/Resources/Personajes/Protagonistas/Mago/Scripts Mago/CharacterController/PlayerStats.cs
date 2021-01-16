using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Start is called before the first frame update

    public float maxHP;
    public float currentHP;
    public float maxMP;
    public float currentMP;
    public int defensaMagica;
    public int defensaFisica;
    public int ataqueMagico;
    public int ataqueFisico;
    public int probCritico;
    public int defensaCriticos;
    public int evasion;
    public int tenacidad;

    GUIManager guiManager;

    void Start()
    {
        this.guiManager = GetComponent<GUIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentMP != maxMP)
        {
            increaseMP(0.005f);
        }
    }

    public void increaseHP(float increase)
    {
        if ((this.currentHP + increase) < this.maxHP)
            this.currentHP += increase;
        else
            this.currentHP = this.maxHP;

        this.guiManager.updateHpBar(this.currentHP, this.maxHP);
    }

    public void decreaseHP(float decrease)
    {
        if ((this.currentHP - decrease) > 0)
            this.currentHP -= decrease;
        else
            this.currentHP = 0;

        this.guiManager.updateHpBar(this.currentHP, this.maxHP);
    }

    public void increaseMP(float increase)
    {
        if ((this.currentMP + increase) < this.maxMP)
            this.currentMP += increase;
        else
            this.currentMP = this.maxMP;

        this.guiManager.updateMpBar(this.currentMP, this.maxMP);
    }

    public void decreaseMP(float decrease)
    {
        if ((this.currentMP - decrease) > 0)
            this.currentMP -= decrease;
        else
            this.currentMP = 0;

        this.guiManager.updateMpBar(this.currentMP, this.maxMP);
    }
}

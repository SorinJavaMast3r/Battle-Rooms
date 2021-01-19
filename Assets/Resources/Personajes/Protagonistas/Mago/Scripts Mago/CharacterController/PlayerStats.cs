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
    public float timeGameOver;

    public GameObject gameOver;
    GUIManager guiManager;

    public bool dead = false;

    void Start()
    {
        this.guiManager = GetComponent<GUIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {            
            if (Time.time > timeGameOver)
            {
                gameOver.SetActive(true);
            }
            return;
        }            

        if (currentHP == 0)
        {
            timeGameOver = Time.time + 5f;
            dead = true;
            Debug.Log("Muerto");
            this.GetComponent<Animator>().Play("Die");
            this.tag = "Dead";
        }

        if (currentMP != maxMP)
        {
            increaseMP(0.005f);
        }

        if (this.name.Equals("Paladin"))
        {
            increaseHP(0.008f);
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

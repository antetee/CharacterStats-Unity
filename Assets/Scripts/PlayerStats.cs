using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    #region Variables
    [Header("Survival Stats")]
    public Stat hunger;
    public Stat thirst;

    [SerializeField] private bool isIncreasingHunger = false;
    [SerializeField] private bool isIncreasingThirst = false;

    [SerializeField] private float increaseHungerDelay;
    [SerializeField] private float increaseThirstDelay;

    [SerializeField] private int increaseHungerAmount;
    [SerializeField] private int increaseThirstAmount;

    [SerializeField] private float increaseHungerRepeat;
    [SerializeField] private float increaseThirstRepeat;

    [SerializeField] private bool starving;
    [SerializeField] private bool dehydrated;
    #endregion

    #region References
    [SerializeField] private StatsManager statsManager = null;
    #endregion

    #region BuiltIn Methods

    public override void Start()
    {
        base.Start();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(2);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            UseMana(10);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            UseStamina(10);
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            Eat(25);
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            Drink(25);
        }
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.T))
        {
            Heal(10);
        }
    }
    #endregion

    #region General Custom Methods
    public override void SetLevelOne()
    {
        base.SetLevelOne();
        hunger.SetMaxValue(100);
        hunger.SetValue(0);
        starving = false;
        increaseHungerAmount = 1;
        increaseHungerDelay = 5;
        increaseHungerRepeat = 2;

        thirst.SetMaxValue(100);
        thirst.SetValue(0);
        dehydrated = false;
        increaseThirstAmount = 1;
        increaseThirstDelay = 5;
        increaseThirstRepeat = 2;

        CheckHealth();
        CheckMana();
        CheckStamina();
        CheckHunger();
        CheckThirst();
    }

    private void UseHunger(int useHungerAmount)
    {
        int hungerAfterUse = hunger.GetValue() + useHungerAmount;
        hunger.SetValue(hungerAfterUse);
        CheckHunger();
    }

    private void UseThirst(int useThirstAmount)
    {
        int thirstAfterUse = thirst.GetValue() + useThirstAmount;
        thirst.SetValue(thirstAfterUse);
        CheckThirst();
    }

    public void Eat(int eatAmount)
    {
        int hungerAfterEat = hunger.GetValue() - eatAmount;
        hunger.SetValue(hungerAfterEat);
        CheckHunger();
    }

    public void Drink(int drinkAmount)
    {
        int thirstAfterDrink = thirst.GetValue() - drinkAmount;
        thirst.SetValue(thirstAfterDrink);
        CheckThirst();
    }
    #endregion

    #region Check Custom Methods
    public override void CheckHealth()
    {
        base.CheckHealth();
        //Update UI
        statsManager.UpdateStatUI(health);
    }

    public override void CheckMana()
    {
        base.CheckMana();
        //UpdateUI
        statsManager.UpdateStatUI(mana);
    }

    public override void CheckStamina()
    {
        base.CheckStamina();
        statsManager.UpdateStatUI(stamina);
    }

    private void CheckHunger()
    {
        hunger.ClampStat();
        if (hunger.GetValue() < hunger.GetMaxValue())
        {
            starving = false;
            if (!isIncreasingHunger)
            {
                isIncreasingHunger = true;
                InvokeRepeating("IncreaseHunger", increaseHungerDelay, increaseHungerRepeat);
            }
        }
        statsManager.UpdateStatUI(hunger);
    }

    private void CheckThirst()
    {
        thirst.ClampStat();
        if (thirst.GetValue() < thirst.GetMaxValue())
        {
            dehydrated = false;
            if (!isIncreasingThirst)
            {
                isIncreasingThirst = true;
                InvokeRepeating("IncreaseThirst", increaseThirstDelay, increaseThirstRepeat);
            }
        }
        statsManager.UpdateStatUI(thirst);
    }
    #endregion

    #region Increase & Decrease Stats Custom Methods
    private void IncreaseHunger()
    {
        if (!hunger.IsStatFull())
        {
            UseHunger(increaseHungerAmount);
        }
        else
        {
            starving = true;
            isIncreasingHunger = false;
            CancelInvoke();
        }
    }

    private void IncreaseThirst()
    {
        if (!thirst.IsStatFull())
        {
            UseThirst(increaseThirstAmount);
        }
        else
        {
            dehydrated = true;
            isIncreasingThirst = false;
            CancelInvoke();
        }
    }

    public override void RegenerateStamina()
    {
        /*
        if (!controller.isRunning)
        {
            base.RegenerateStamina();
        }
        */
    }

    #endregion
}

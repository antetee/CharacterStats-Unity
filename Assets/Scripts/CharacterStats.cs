using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    #region Variables
    [Header("General")]
    public string characterName;
    public int level;

    [Header("Base Stats")]
    public Stat health;
    public Stat mana;
    public Stat stamina;

    [Header("Booleans")]
    public bool isDead = false;
    public bool isInCombat = false;

    [Header("Regeneration Variables")]
    [SerializeField] private bool isRegeneratingMana = false;
    [SerializeField] private bool isRegeneratingStamina = false;

    [SerializeField] private float regenManaDelay;
    [SerializeField] private float regenStaminaDelay;

    [SerializeField] private int regenManaAmount;
    [SerializeField] private int regenStaminaAmount;

    [SerializeField] private float regenManaRepeat;
    [SerializeField] private float regenStaminaRepeat;
    #endregion

    #region BuiltIn Methods
    public virtual void Start()
    {
        SetLevelOne();
    }
    #endregion

    #region General Custom Methods
    public virtual void Die()
    {
        //Die some way.
        Debug.Log("You died. :|");
        isDead = true;
    }

    public virtual void SetLevelOne()
    {
        level = 1;
        isInCombat = false;

        health.SetMaxValue(10);
        health.ResetStat();
        isDead = false;

        mana.SetMaxValue(25);
        mana.ResetStat();
        isRegeneratingMana = false;
        regenManaAmount = 1;
        regenManaDelay = 4;
        regenManaRepeat = 1;

        stamina.SetMaxValue(25);
        stamina.ResetStat();
        isRegeneratingStamina = false;
        regenStaminaAmount = 1;
        regenStaminaDelay = 4;
        regenStaminaRepeat = 1;
    }

    public void TakeDamage(int damage)
    {
        int healthAfterDamage = health.GetValue() - damage;
        health.SetValue(healthAfterDamage);
        CheckHealth();
    }

    public void UseMana(int usedManaAmount)
    {
        int manaAfterUse = mana.GetValue() - usedManaAmount;
        Debug.Log("Mana used: " + usedManaAmount);
        mana.SetValue(manaAfterUse);
        CheckMana();
    }

    public void UseStamina(int usedStaminaAmount)
    {
        int staminaAfterUse = stamina.GetValue() - usedStaminaAmount;
        Debug.Log("Stamina used: " + usedStaminaAmount);
        stamina.SetValue(staminaAfterUse);
        CheckStamina();
    }

    public void Heal(int healAmount)
    {
        int healthAfterHeal = health.GetValue() + healAmount;
        health.SetValue(healthAfterHeal);
        CheckHealth();
    }

    public void AddMana(int manaAddAmount)
    {
        int manaAfterAdd = mana.GetValue() + manaAddAmount;
        mana.SetValue(manaAfterAdd);
        CheckMana();
    }

    public void AddStamina(int staminaAddAmount)
    {
        int staminaAfterAdd = stamina.GetValue() + staminaAddAmount;
        stamina.SetValue(staminaAfterAdd);
        CheckStamina();
    }
    #endregion

    #region Check Custom Methods
    public virtual void CheckHealth()
    {
        health.ClampStat();
        //If health is empty.
        if (health.IsStatEmpty())
            Die();
    }

    public virtual void CheckMana()
    {
        mana.ClampStat();
        //If mana needs to be regened.
        if (!mana.IsStatFull())
        {
            //Start regen.
            if (!isInCombat)
            {
                if (!isRegeneratingMana)
                {
                    isRegeneratingMana = true;
                    InvokeRepeating("RegenerateMana", regenManaDelay, regenManaRepeat);
                }
            }
        }
    }

    public virtual void CheckStamina()
    {
        stamina.ClampStat();
        //If stamina needs to be regened.
        if (!stamina.IsStatFull())
        {
            if (!isInCombat)
            {
                if (!isRegeneratingStamina)
                {
                    isRegeneratingStamina = true;
                    InvokeRepeating("RegenerateStamina", regenStaminaDelay, regenStaminaRepeat);
                }
            }
        }
    }
    #endregion

    #region Regenerate Custom Methods

    private void RegenerateMana()
    {
        if (!mana.IsStatFull())
            AddMana(regenManaAmount);
        else
        {
            isRegeneratingMana = false;
            CancelInvoke();
        }
    }

    public virtual void RegenerateStamina()
    {
        if (!stamina.IsStatFull())
            AddStamina(regenStaminaAmount);
        else
        {
            isRegeneratingStamina = false;
            CancelInvoke();
        }
    }
    #endregion
}

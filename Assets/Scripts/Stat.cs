using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    #region Variables
    public int statID;

    [SerializeField] private string statName;

    [SerializeField] private int baseValue;
    [SerializeField] private int maxValue;

    private List<int> modifiers = new List<int>();

    [SerializeField] private bool statEmpty = false;
    [SerializeField] private bool statFull = false;
    #endregion

    #region Custom Methods
    public int GetValue()
    {
        return baseValue;
    }

    public int GetMaxValue()
    {
        int finalValue = maxValue;
        modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }

    public string GetName()
    {
        return statName;
    }

    public void SetValue(int newValue)
    {
        baseValue = newValue;
    }

    public void SetMaxValue(int newValue)
    {
        maxValue = newValue;
    }

    public void ClampStat()
    {
        if (baseValue <= 0)
        {
            baseValue = 0;
            statEmpty = true;
        }
        else if (baseValue >= GetMaxValue())
        {
            baseValue = GetMaxValue();
            statFull = true;
        }
        else if (baseValue > 1 && baseValue < GetMaxValue())
        {
            statEmpty = false;
            statFull = false;
        }
    }

    public void ResetStat()
    {
        baseValue = GetMaxValue();
        ClampStat();
    }

    public bool IsStatEmpty()
    {
        return statEmpty;
    }

    public bool IsStatFull()
    {
        return statFull;
    }

    public void AddModifier(int modifier)
    {
        if (modifier != 0)
            modifiers.Add(modifier);
    }

    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
            modifiers.Remove(modifier);
    }
    #endregion
}

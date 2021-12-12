using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private List<StatUI> statUI;
    #endregion

    #region Built-In Methods
    private void Start()
    {
        InitVariables();
    }
    #endregion

    #region Custom Methods
    private void InitVariables()
    {
        foreach(Transform child in transform)
        {
            StatUI childStatUI = child.GetComponent<StatUI>();

            if(childStatUI != null)
                statUI.Add(child.GetComponent<StatUI>());
        }
    }

    public void UpdateStatUI(Stat stat)
    {
        int updatedStatID = stat.statID;
        statUI[updatedStatID - 1].UpdateStatUI(stat.GetValue(), stat.GetMaxValue(), stat.GetName());
    }
    #endregion
}

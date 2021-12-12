using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : MonoBehaviour
{
    #region References
    [SerializeField] private Image fillBarImage = null;
    [SerializeField] private Text amountText = null;
    [SerializeField] private Text statNameText = null;

    #endregion

    #region Custom Methods
    public void UpdateStatUI(int statBaseValue, int statMaxValue, string statName)
    {
        fillBarImage.rectTransform.localScale = new Vector3((float)statBaseValue / (float)statMaxValue, 1, 1);
        amountText.text = statBaseValue.ToString();
        statNameText.text = statName;
    }
    #endregion
}

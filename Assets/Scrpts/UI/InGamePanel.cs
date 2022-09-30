using Scripts.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Scripts.Ui
{

    public class InGamePanel : UIPanel
    {
        [SerializeField] TextMeshProUGUI _awareTextForFeverMode;

        public override void Initialize(UIManager uiManager)
        {
            base.Initialize(uiManager);
        }

        public override void ShowPanel()
        {
            base.ShowPanel();
        }

        public override void HidePanel()
        {
            base.HidePanel();
        }

        public void SetFeverModeText(bool isActive)
        {
            _awareTextForFeverMode.gameObject.SetActive(isActive);
        }
    }

}
using Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Ui
{
    public class FinishPanel : UIPanel
    {

        [SerializeField] private GameObject _winPanel;
        [SerializeField] private GameObject _losePanel;

        public override void Initialize(UIManager uiManager)
        {
            base.Initialize(uiManager);
            GameManager.EventManager.OnGameStarted += RefreshPanels;
            GameManager.EventManager.OnLevelFinished += SetWinLosePanel;
        }
        private void OnDestroy()
        {
            GameManager.EventManager.OnGameStarted -= RefreshPanels;
            GameManager.EventManager.OnLevelFinished -= SetWinLosePanel;
        }

        public override void ShowPanel()
        {
            base.ShowPanel();
        }

        private void RefreshPanels()
        {
            _winPanel.SetActive(false);
            _losePanel.SetActive(false);
        }

        private void SetWinLosePanel(bool isSuccess)
        {
            StartCoroutine(SetWinLosePanelCo(isSuccess));
        }

        private IEnumerator SetWinLosePanelCo(bool isSuccess)
        {
            yield return new WaitForSeconds(0.25f);

            _winPanel.SetActive(isSuccess);
            _losePanel.SetActive(!isSuccess);
        }
    }
}

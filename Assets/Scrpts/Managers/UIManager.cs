using Scripts.Ui;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.Managers
{

    public class UIManager : CustomBehaviour
    {
        public MainMenuPanel MainMenuPanel => _mainMenuPanel;
        public InGamePanel InGamePanel => _inGamePanel;
        public FinishPanel FinishPanel => _finishPanel;
        [SerializeField] private MainMenuPanel _mainMenuPanel;
        [SerializeField] private FinishPanel _finishPanel;
        [SerializeField] private InGamePanel _inGamePanel;
        private List<UIPanel> UIPanels;

        public override void Initialize(GameManager gameManager)
        {
            base.Initialize(gameManager);
            UIPanels = new List<UIPanel> { _mainMenuPanel , _finishPanel,_inGamePanel};

            UIPanels.ForEach(x =>
            {
                x.Initialize(this);
                x.gameObject.SetActive(false);
            });
            
        }
    }
}
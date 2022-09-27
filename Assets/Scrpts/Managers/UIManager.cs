using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

namespace Scripts.Managers
{

    public class UIManager : CustomBehaviour
    {
        public MainMenuPanel MainMenuPanel => _mainMenuPanel;
        [SerializeField] private MainMenuPanel _mainMenuPanel;

        private List<UIPanel> UIPanels;

        public override void Initialize(GameManager gameManager)
        {
            base.Initialize(gameManager);
            UIPanels = new List<UIPanel> { _mainMenuPanel };

            UIPanels.ForEach(x =>
            {
                x.Initialize(this);
                x.gameObject.SetActive(false);
            });
            //hudPanel.ShowPanel();

        }
    }
}
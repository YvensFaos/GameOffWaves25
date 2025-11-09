using Actors;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthDisplayUI : MonoBehaviour
    {
        [SerializeField] private Image healthBar;
        [SerializeField] private TextMeshProUGUI healthText;

        private NavalActor _actor;

        public void Initialize(NavalActor actor)
        {
            _actor = actor;
            UpdateValues();
        }

        private void OnEnable()
        {
            if (_actor != null) UpdateValues();
        }

        private void UpdateValues()
        {
            var ratio = _actor.GetHealthRatio();
            healthBar.fillAmount = ratio;
            healthText.text = $"{_actor.GetCurrentHealth()}/{_actor.GetMaxHealth()}";
        }
    }
}
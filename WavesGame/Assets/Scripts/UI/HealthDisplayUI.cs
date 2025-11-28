/*
 * Copyright (c) 2025 Yvens R Serpa [https://github.com/YvensFaos/]
 *
 * This work is licensed under the Creative Commons Attribution 4.0 International License.
 * To view a copy of this license, visit http://creativecommons.org/licenses/by/4.0/
 * or see the LICENSE file in the root directory of this repository.
 */

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
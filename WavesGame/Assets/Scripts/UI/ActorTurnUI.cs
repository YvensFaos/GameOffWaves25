/*
 * Copyright (c) 2025 Yvens R Serpa [https://github.com/YvensFaos/]
 *
 * This work is licensed under the Creative Commons Attribution 4.0 International License.
 * To view a copy of this license, visit http://creativecommons.org/licenses/by/4.0/
 * or see the LICENSE file in the root directory of this repository.
 */

using System;
using Actors;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ActorTurnUI : MonoBehaviour, IComparable<ActorTurnUI>
    {
        [SerializeField] private Image holderImage;
        [SerializeField] private Image actorImage;
        [SerializeField, ReadOnly] private NavalShip navalShip;
        [SerializeField, ReadOnly] private NavalShipSo navalShipSo;

        [Header("References")]
        [SerializeField] private Sprite availableActorHolderSprite;
        [SerializeField] private Sprite unavailableActorHolderSprite;
        
        public void Initialize(NavalShip newNavalShip)
        {
            navalShip = newNavalShip;
            navalShipSo = newNavalShip.ShipData;
            actorImage.sprite = navalShipSo.shipSprite;
            ToggleAvailability(false);
        }

        public void ToggleAvailability(bool available)
        {
            holderImage.sprite = available ? availableActorHolderSprite : unavailableActorHolderSprite;
        }

        public int CompareTo(ActorTurnUI other)
        {
            return navalShipSo.stats.speed.Two.CompareTo(other.navalShipSo.stats.speed.Two);
        }
        
        public NavalShip NavalShip => navalShip;
    }
}
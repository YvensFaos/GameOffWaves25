/*
 * Copyright (c) 2025 Yvens R Serpa [https://github.com/YvensFaos/]
 *
 * This work is licensed under the Creative Commons Attribution 4.0 International License.
 * To view a copy of this license, visit http://creativecommons.org/licenses/by/4.0/
 * or see the LICENSE file in the root directory of this repository.
 */

using Core;
using TMPro;
using UnityEngine;
using UUtils;

namespace Debugging
{
    public class CursorDebugDisplay : MonoBehaviour
    {
        [SerializeField]
        private CursorController cursorController;
        [SerializeField]
        private TextMeshProUGUI cursorIndexText;

        private void Awake()
        {
            if (!GameManager.GetSingleton().GetSettings().debugCursorInformation)
            {
                Destroy(this);
                return;
            }
            
            AssessUtils.CheckRequirement(ref cursorController, this);
            AssessUtils.CheckRequirement(ref cursorIndexText, this);
        }

        private void Update()
        {
            cursorIndexText.text = cursorController.GetIndex().ToString();
        }
    }
}

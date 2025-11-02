using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UUtils;

namespace Core
{
    public class PlayerController : WeakSingleton<PlayerController>
    {
        [SerializeField] private PlayerInput input;

        public Action<Vector2> onNavigateAction;
        
        public void OnNavigate(InputAction.CallbackContext context)
        {
            var readValue = context.ReadValue<Vector2>();
            onNavigateAction?.Invoke(readValue);
        }
    }
}
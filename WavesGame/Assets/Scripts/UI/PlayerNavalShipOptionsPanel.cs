using Core;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UUtils;

namespace UI
{
    public class PlayerNavalShipOptionsPanel : MonoBehaviour
    {
        [SerializeField] private CursorController cursorController;
        [SerializeField] private RectTransform selfRectTransform;
        [SerializeField] private Button initialButton;

        private void Awake()
        {
            AssessUtils.CheckRequirement(ref cursorController, this);
            AssessUtils.CheckRequirement(ref selfRectTransform, this);
        }

        private void OnEnable()
        {
            selfRectTransform.localScale = Vector3.zero;
            selfRectTransform.DOScale(Vector3.one, 0.3f).OnComplete(() =>
            {
                EventSystem.current.SetSelectedGameObject(initialButton.gameObject);
                PlayerController.GetSingleton().onCancel += Cancel;
            });
        }

        private void OnDisable()
        {
            PlayerController.GetSingleton().onCancel -= Cancel;
        }

        public void Move()
        {
        }

        public void Attack()
        {
        }

        public void Info()
        {
        }

        public void Cancel()
        {
            cursorController.CancelSelectedActor();
        }
    }
}
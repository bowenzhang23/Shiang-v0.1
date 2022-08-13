
using Ink.Runtime;
using System;
using UnityEngine;

namespace Shiang
{
    [RequireComponent(typeof(CollisionDetector))]
    public class DialogueTrigger : MonoBehaviour, IStatic
    {
        CollisionDetector _colliDetec;
        
        [SerializeField] GameObject _dialoguePanel;

        public Story CurrentStory { get; set; }

        public void Idle() { }

        void Awake()
        {
            _colliDetec = GetComponent<CollisionDetector>();
            _colliDetec.OnPlayerEnter += HandlePlayerEnter;
            _colliDetec.OnPlayerExit += HandlePlayerExit;
        }

        private void HandlePlayerEnter()
        {
            UiManagement.CurrentDialogueActivator = gameObject;
        }

        private void HandlePlayerExit()
        {
            _dialoguePanel.SetActive(false);
            UiManagement.CurrentDialogueActivator = null;
        }

        void Update()
        {
            if (_colliDetec.PlayerDetected && InputController.Instance.Interact)
            {
                _dialoguePanel.SetActive(true);
                DialoguePanel.Instance.StartStory(CurrentStory);
            }
        }
    }
}

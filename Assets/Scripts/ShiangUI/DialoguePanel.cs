
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

namespace Shiang
{
	[System.Serializable]
	public class DialogueOption
    {
		public Button button;
		public TMP_Text text;
    }

	public class DialoguePanel : GenericSingleton<DialoguePanel>
	{
		Story _inkStory;

		[SerializeField] TMP_Text _currentText;
		[SerializeField] DialogueOption[] _dialogueOptions;
		
		public void StartStory(Story story)
        {
			_inkStory = story;
#if UNITY_EDITOR
			Debug.Log($"Current #options = {_inkStory.currentChoices.Count}");
			if (_inkStory.currentChoices.Count > _dialogueOptions.Length)
				Debug.LogWarning("Not enough options for the current dialogue!");
#endif
			RefreshView();
		}

		void RefreshView()
        {
			if (_inkStory.canContinue)
			{
				ClearUI();
				EnableUI(0);

				string newline = _inkStory.Continue().Trim();

				bool findActor = false;
				foreach (var tag in _inkStory.currentTags)
				{
					if (tag.StartsWith("Actor."))
                    {
						var actorName = tag.Substring("Actor.".Length);
						_currentText.text = $"[{actorName}] {newline}";
						findActor = true;
					}
                }

				if (!findActor)
					_currentText.text = newline;

				if (_inkStory.currentChoices.Count > 0)
				{
					RefreshView();
				}
				else
				{
					_dialogueOptions[0].text.text = "Continue..";
					_dialogueOptions[0].button.onClick.AddListener(delegate
					{
						RefreshView();
					});
					Utils.SelectAndHighlightButton(_dialogueOptions[0].button);
				}
			}

			else if (_inkStory.currentChoices.Count > 0)
			{
				ClearUI();

				for (int i = 0; i < _inkStory.currentChoices.Count; ++i)
				{
					EnableUI(i);

					Choice choice = _inkStory.currentChoices[i];
					_dialogueOptions[i].text.text = choice.text;
					_dialogueOptions[i].button.onClick.AddListener(delegate
					{
						OnChoiceButtonClicked(choice);
					});
				}
				Utils.SelectAndHighlightButton(_dialogueOptions[0].button);
			}

			else
            {
				ClearUI();
				EnableUI(0);

				_currentText.text = "THE END";
				_dialogueOptions[0].text.text = "Quit";
				_dialogueOptions[0].button.onClick.AddListener(delegate
				{
					gameObject.SetActive(false);
				});
				Utils.SelectAndHighlightButton(_dialogueOptions[0].button);
			}
		}

		void ClearUI()
        {
			foreach (var option in _dialogueOptions)
			{
				option.text.enabled = false;
				option.button.enabled = false;
				option.button.image.color = Color.clear;
				option.button.onClick.RemoveAllListeners();
			}
		}

		void EnableUI(int i)
        {
			_dialogueOptions[i].text.enabled = true;
			_dialogueOptions[i].button.enabled = true;
			_dialogueOptions[i].button.image.color = Color.white;
		}

		void OnChoiceButtonClicked(Choice choice)
        {
			_inkStory.ChooseChoiceIndex(choice.index);
			RefreshView();
        }
    }
}
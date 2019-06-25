// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace EscapeModules
{
    /// <summary>
    /// Show and hide hint. Add number of hints and set it to Data script
    /// </summary>
    public class HintsController : MonoBehaviour
    {
        [SerializeField] private bool _AreHintsForCurrentLevel;
        [SerializeField] private bool _AreHintsLimited;
        [SerializeField] private int _numberOfHints;
        [SerializeField] private Button _buttonShowHint;
        [SerializeField] private GameObject _hintParent;
        [SerializeField] private Text _numberOfHintsTextField;
        [SerializeField] private Image _hintImage;

        private bool _isShow;
        private Button _hideButton;


        void Awake()
        {
            if (_hintParent == null) Debug.LogError("Add Image Hint Object " + gameObject.name);
            if (_buttonShowHint == null) Debug.LogError("EscapeModules: Button Show Hint can't be null " + gameObject.name);
            if (_numberOfHintsTextField == null) Debug.LogError("EscapeModules: NumberOfHintsText can't be null " + gameObject.name);
            if (_hintImage == null) Debug.LogError("EscapeModules: Hint Image can't be null " + gameObject.name);
            Data.IsHintsForCurrentLevel = _AreHintsForCurrentLevel;
            Data.SetHints(_AreHintsLimited, _numberOfHints);
            if (_numberOfHintsTextField != null) _numberOfHintsTextField.text = Data.IsHintsLimited ? Data.NumberOfHits.ToString() : "";
            _hintParent.SetActive(false);
            _hideButton = _hintParent.GetComponent<Button>() ?? _hintParent.AddComponent<Button>();
            (transform as RectTransform).SetAsLastSibling();
        }

        void OnEnable()
        {
            _hideButton.onClick.AddListener(HideHint);
            _buttonShowHint.onClick.AddListener(ShowHint);
        }

        void OnDisable()
        {
            _hideButton.onClick.RemoveAllListeners();
            _buttonShowHint.onClick.RemoveAllListeners();
        }

        private void ShowHint()
        {
            if (PlayerController.Instance.IsLock) return;
            if (Data.NumberOfHits <= 0 && Data.IsHintsLimited) return;
            string impg = Data.CurrentLevelName;
            //Texture2D texture = Resources.Load(SceneManager.GetActiveScene().name) as Texture2D ?? Resources.Load(impg) as Texture2D ?? Resources.Load("NoMap") as Texture2D;
            //Had to comment this out due to error in script. that would not compile
            //Sprite newSprite = new Sprite();
            //if (texture != null)
            //    newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, .5f));
            //_hintImage.sprite = newSprite;
            if (!_isShow)
            {
                _hintParent.SetActive(true);
                _isShow = true;
                PlayerController.Instance.IsLock = true;
                PlayerController.Instance.IsDrag = true;
            }
            else
            {
                HideHint();
            }
        }

        private void HideHint()
        {
            _hintParent.SetActive(false);
            _isShow = false;
            PlayerController.Instance.IsLock = false;
            PlayerController.Instance.IsDrag = false;
            if (Data.NumberOfHits > 0 && Data.IsHintsLimited)
            {
                Data.DecrementHints();
                _numberOfHintsTextField.text = _AreHintsLimited && Data.NumberOfHits >= 0 ? Data.NumberOfHits.ToString() : "";
            }
        }

        public void AddShowHitsCounter()
        {
            Data.IncrementHints();
        }
    }
}

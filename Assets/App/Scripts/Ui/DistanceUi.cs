using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Ui {
    public class DistanceUi : MonoBehaviour {
        
        [SerializeField]
        private RectTransform _rectTransform = null;
        
        [SerializeField]
        private TextMeshProUGUI _text = null;

        [SerializeField]
        private Image _icon = null;

        public void SetData(Vector3 screenPoint, float distance) {
            transform.position = CalcDistancePosition(screenPoint);
            _icon.gameObject.SetActive(!IsVisible(screenPoint));
            _text.text = ((int) distance).ToString();

            PlaceText();
        }
        
        
        private Vector3 CalcDistancePosition(Vector3 screenPoint) {
            var halfWidth = _rectTransform.sizeDelta.x / 2f;
            var halfHeight = _rectTransform.sizeDelta.y / 2f;
            
            if (screenPoint.z < 0) {
                var x = screenPoint.x < Screen.width / 2f ? Screen.width - halfWidth : halfWidth;
                var y = Mathf.Clamp(-screenPoint.y, halfHeight, Screen.height - halfHeight);
                return new Vector3(x, y, 0);
            } else {
                var x = Mathf.Clamp(screenPoint.x, halfWidth, Screen.width - halfWidth);
                var y = Mathf.Clamp(screenPoint.y, halfHeight, Screen.height - halfHeight);
                return new Vector3(x, y, 0);
            }
        }
        
        private bool IsVisible(Vector3 screenPoint) {
            if (screenPoint.z < 0) return false;
            return screenPoint.x >= 0 && screenPoint.x <= Screen.width && screenPoint.y >= 0 &&
                   screenPoint.y <= Screen.height;
        }

        private void PlaceText() {
            var pos = transform.position;
            var halfWidth = _rectTransform.sizeDelta.x / 2f;
            var halfHeight = _rectTransform.sizeDelta.y / 2f;

            var textHalfWidth = _text.rectTransform.sizeDelta.x / 2f;
            var textHeight = _text.rectTransform.sizeDelta.y;

            if (pos.x < textHalfWidth) {
                _text.transform.localPosition = new Vector3(halfWidth + textHalfWidth, 0, 0);
                _text.alignment = TextAlignmentOptions.Left;
            } else if (pos.x > Screen.width - (textHalfWidth)) {
                _text.transform.localPosition = new Vector3(- (halfWidth + textHalfWidth), 0, 0);
                _text.alignment = TextAlignmentOptions.Right;
            } else {
                
                _text.alignment = TextAlignmentOptions.Center;
                
                if (pos.y > Screen.height - (textHeight + halfHeight)) {
                    _text.transform.localPosition = new Vector3(0, -(textHeight / 2f + halfHeight), 0);
                } else {
                    _text.transform.localPosition = new Vector3(0, textHeight / 2f + halfHeight, 0);
                }
            }
        }
    }
}
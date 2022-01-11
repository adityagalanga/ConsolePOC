using UnityEngine;

namespace Nagih
{
    public class PopupManager : MonoBehaviour
    {
        private GameObject _canvas;
        private PopupInfo _info;
        private PopupSelection _selection;
        private PopupNotification _notification;
        private PopupConsent _consent;

        public PopupInfo Info { get { return _info.SetDefault(); } }
        public PopupSelection Selection { get { return _selection.SetDefault(); } }
        public PopupNotification Notification { get { return _notification.SetDefault(); } }
        public PopupConsent Consent { get { return _consent.SetDefault(); } }

        public void LoadAsset()
        {
            _canvas = Instantiate(Resources.Load<GameObject>(Const.RESLOC_POPUP_CANVAS), transform);
            _info = Instantiate(Resources.Load<GameObject>(Const.RESLOC_POPUP_INFO), _canvas.transform).GetComponent<PopupInfo>();
            _selection = Instantiate(Resources.Load<GameObject>(Const.RESLOC_POPUP_SELECTION), _canvas.transform).GetComponent<PopupSelection>();
            _notification = Instantiate(Resources.Load<GameObject>(Const.RESLOC_POPUP_NOTIFICATION), _canvas.transform).GetComponent<PopupNotification>();
            _consent = Instantiate(Resources.Load<GameObject>(Const.RESLOC_POPUP_CONSENT), _canvas.transform).GetComponent<PopupConsent>();
        }
    }
}
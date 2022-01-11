using System.Collections.Generic;

namespace Nagih
{
    public static class Const
    {
        // URL
#if ENV_STAG
        public const string URL_BASE = "https://gamesmanager.staging.polytron.angkasa.id/";
        public const bool DEBUG_LOG_ENABLED = true;
#elif ENV_PROD
        public const string URL_BASE = "https://gamesmanager.nagihgames.com/";
        public const bool DEBUG_LOG_ENABLED = false;
#else
        //public const string URL_BASE = "http://lasagna.fira.id:1300/";
        public const string URL_BASE = "http://gamesmanager.lasagna.fira.id/";
        public const bool DEBUG_LOG_ENABLED = true;
#endif
        public const string VERSION = "v2";
        public const string URL_REQUEST = URL_BASE + "api/" + VERSION + "/request";
        public const string URL_GET_CONTENT = "https://nagihgames.com/api/v1/request";
        // --

        // config
#if ENV_STAG || ENV_PROD
        public const bool USE_POPUP_WARNING = true;
#else
        public const bool USE_POPUP_WARNING = false;
#endif
        public const bool DEFAULT_CONSENT = true;
        public const int TIMEOUT_DURATION = 10;
        public const float DISTANCE_MINIMUM = 0.01f;
        public const string AUTH_USERNAME = "GameManager";
        public const string AUTH_PASSWORD = "=64M3m4N463r@N461h";
        public const string AUTH_FIRA_USERNAME = "Nagihgames";
        public const string AUTH_FIRA_PASSWORD = "nagih132";
        public const string GAME_SECRET = ""; // secret untuk enkripsi di device, butuh 32 bit
        public const string GAME_ID = ""; // id game untuk rekuest ke server
#if ENV_STAG
        public const string SERVER_KEY_SECRET = "";
#elif ENV_PROD
        public const string SERVER_KEY_SECRET = "";
#else
        public const string SERVER_KEY_SECRET = ""; // digenerate dari server
#endif
        // --

        // request type
        public const string NAGIH_POLICY = "get_privacy";
        public const string UTILITY_GET_TIME = "utility.get_time";
        public const string AUTH_LOGIN = "auth.login";
        public const string AUTH_REFRESH_TOKEN = "auth.refresh_token";
        public const string USER_GET_PROFILE = "user.get_user_profile";
        public const string USER_SET_PROFILE = "user.set_user_profile";
        public static readonly List<string> REQUEST_CHECK_ID = new List<string>
        {
            AUTH_LOGIN,
            USER_GET_PROFILE,
            USER_SET_PROFILE
        };
        // --
        
        // form type
        public const string FORM_APP_ID = "nagih";
        public const string FORM_DOCTYPE_POLICY = "policy";
        // --

        // tutorial
        // --

        // analytics    
        // --

        // ADS
#if UNITY_ANDROID
        public const string UNITYADS_GAME_ID = "3870357"; // ini dummy, musti diubah
#elif UNITY_IOS
        public const string UNITYADS_GAME_ID = "3870356";
#endif
        public const string UNITYADS_INTERSTITIAL = "video";
        public const string UNITYADS_REWARDED_VIDEO = "rewardedVideo";
        // --

        // Animation duration
        public const float DUR_LOADING_MINIMUM = 2f;
        public const float DUR_FADEIN = 0.25f;
        public const float DUR_FADEOUT = 0.25f;
        // --

        // Animation key
        public const string ANIMKEY_SELECTABLE_NORMAL = "Normal";
        public const string ANIMKEY_SELECTABLE_SELECTED = "Selected";
        // -- 

        // Resources Location
        public const string RESLOC_DATA_GAMEINIT = "Data/GameDataSO";
        public const string RESLOC_DATA_LEAN = "Data/LeanLocalization";

        public const string RESLOC_LOADING_SCENE = "Loading/SceneLoading";
        public const string RESLOC_LOADING_FRONT = "Loading/FrontLoading";
        public const string RESLOC_LOADING_BACK = "Loading/BackLoading";

        public const string RESLOC_POPUP_CANVAS = "Popup/PopupCanvas";
        public const string RESLOC_POPUP_INFO = "Popup/PopupInfo";
        public const string RESLOC_POPUP_SELECTION = "Popup/PopupSelection";
        public const string RESLOC_POPUP_NOTIFICATION = "Popup/PopupNotification";
        public const string RESLOC_POPUP_CONSENT = "Popup/PopupConsent";

        public const string RESLOC_TUTORIAL_CANVAS = "Tutorial/TutorialCanvas";
        public const string RESLOC_TUTORIAL_BOX = "Tutorial/TutorialBox";
        public const string RESLOC_TUTORIAL_ARROW = "Tutorial/TutorialArrow";
        public const string RESLOC_TUTORIAL_MASK = "Tutorial/TutorialMask";
        public const string RESLOC_TUTORIAL_OVERLAY = "Tutorial/TutorialOverlay";
        // --

        // PARAMETER
        public const string PARAM_TOKEN = "tkn";
        public const string PARAM_USER = "usr";
        // --

        // function
        public static bool UseCypher()
        {
            return VERSION == "v2";
        }
        // --
    }
}
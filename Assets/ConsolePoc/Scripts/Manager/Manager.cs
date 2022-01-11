using System.Threading.Tasks;
using UnityEngine;

namespace Nagih
{
    public class Manager : Singleton<Manager>
    {
#if !UNITY_WEBGL
        [HideInInspector] public AdsManager Ads;
        [HideInInspector] public AddressableManager Addressable;
#endif
        [HideInInspector] public SceneManager Scene;
        [HideInInspector] public PopupManager Popup;
        [HideInInspector] public RequestManager Request;
        [HideInInspector] public PlayerPrefManager PlayerPref;
        [HideInInspector] public LoginManager Login;
        [HideInInspector] public AudioManager Audio;
        [HideInInspector] public TutorialManager Tutorial;
        [HideInInspector] internal WorkerManager Worker;

        public GameObject FrontLoading { get; private set; }
        public SceneLoading SceneLoading { get; private set; }

        protected override void OnAwake()
        {
#if !UNITY_WEBGL
            Ads = gameObject.AddComponent<AdsManager>();
            Addressable = gameObject.AddComponent<AddressableManager>();
#endif

            Scene = gameObject.AddComponent<SceneManager>();
            Popup = gameObject.AddComponent<PopupManager>();

            Request = gameObject.AddComponent<RequestManager>();
            PlayerPref = gameObject.AddComponent<PlayerPrefManager>();
            Login = gameObject.AddComponent<LoginManager>();

            Audio = gameObject.AddComponent<AudioManager>();
            Tutorial = gameObject.AddComponent<TutorialManager>();

            Worker = gameObject.AddComponent<WorkerManager>();
        }

        public void Initialize()
        {
            SceneLoading = Instantiate(Resources.Load<GameObject>(Const.RESLOC_LOADING_SCENE), transform).GetComponent<SceneLoading>();
            FrontLoading = Instantiate(Resources.Load<GameObject>(Const.RESLOC_LOADING_FRONT), transform);
        }

        public void LoadAsset()
        {
            Scene.LoadAsset(SceneLoading);
            Popup.LoadAsset();
            Request.LoadAsset(FrontLoading);
            Audio.LoadAsset();
        }

        public void ShowReview()
        {
            //// start preloading the review prompt in the background
            //var playReviewInfoAsyncOperation = Review.RequestReviewFlow();

            //// define a callback after the preloading is done
            //playReviewInfoAsyncOperation.Completed += playReviewInfoAsync => {
            //    if (playReviewInfoAsync.Error == ReviewErrorCode.NoError)
            //    {
            //        // display the review prompt
            //        var playReviewInfo = playReviewInfoAsync.GetResult();
            //        Review.LaunchReviewFlow(playReviewInfo);
            //        Debug.Log("REVIEW SHOWN");
            //    }
            //    else
            //    {
            //        // handle error when loading review prompt
            //        Debug.Log("Can't Show Review");
            //    }
            //};
        }
    }
}
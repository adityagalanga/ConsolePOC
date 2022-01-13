using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace Nagih
{
    public class SplashScreen : ScreenBase
    {
#pragma warning disable CS0649
        [SerializeField] private Image Background;
        [SerializeField] private Image Logo;
        [SerializeField] private float BackgroundFadeInDuration = 1f;
        [SerializeField] private float LogoFadeInDuration = 1f;
        [SerializeField] private float ShowDuration = 0.5f;
        [SerializeField] private float FadeOutDuration = 1f;
#pragma warning restore CS0649

        protected override void Awake()
        {
            Background.SetAlpha(0f);
            Logo.SetAlpha(0f);
            base.Awake();
        }

        protected override void Start()
        {
            StartCoroutine(FadeRoutine());
            base.Start();
        }

        private IEnumerator FadeRoutine()
        {
            while (Background.color.a < 1f)
            {
                Background.ModifyAlpha(Time.deltaTime / BackgroundFadeInDuration);
                yield return null;
            }

            while (Logo.color.a < 1f)
            {
                Logo.ModifyAlpha(Time.deltaTime / LogoFadeInDuration);
                yield return null;
            }

            yield return new WaitForSeconds(ShowDuration);

            while (Background.color.a > 0f && Logo.color.a > 0f)
            {
                Background.ModifyAlpha(-Time.deltaTime / FadeOutDuration);
                Logo.ModifyAlpha(-Time.deltaTime / FadeOutDuration);
                yield return null;
            }

            StartCoroutine(Initialize());
        }

        private IEnumerator Initialize()
        {
            Manager.GetInstance().Initialize();

            SceneLoading loading = Manager.GetInstance().SceneLoading;
            float duration = Const.DUR_LOADING_MINIMUM;

            loading.Show(duration);
            loading.AddMaxProgress(0.1f);

            yield return new WaitForSeconds(Const.DUR_FADEIN);

            DataSelf.GetInstance().Initialize();
            DataStatic.GetInstance().Initialize();
            loading.AddMaxProgress(0.1f);

            Manager.GetInstance().LoadAsset();
            loading.AddMaxProgress(0.1f);

            Login(loading);
        }

        private void Login(SceneLoading loading)
        {
            //Manager.GetInstance().Login.DoLoginSequence(OnComplete, OnEverySequence);
            OnComplete();

            void OnComplete()
            {
                loading.SetMaxProgress(1f);
                Manager.GetInstance().Scene.ChangeScene(Enum.Scene.Game);
            }

            //void OnEverySequence()
            //{
            //    loading.AddMaxProgress(0.1f);
            //}
        }
    }
}
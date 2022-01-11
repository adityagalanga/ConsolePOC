using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Nagih
{
    public class SceneLoading : MonoBehaviour
    {
#pragma warning disable CS0649
        [SerializeField] private Image Slider;
        [SerializeField] private Text ProgressText;
        [SerializeField] private Animator Animator;
#pragma warning restore CS0649

        private float _progress;
        private float _maxProgress;
        private IEnumerator _routine;

        public void Show(float showSeconds)
        {
            _progress = 0f;
            _maxProgress = 0f;

            SetSlider(_progress);
            gameObject.SetActive(true);
            Animator.SetTrigger("FadeIn");

            if (_routine != null) StopCoroutine(_routine);
            _routine = LoadingRoutine(showSeconds);
            StartCoroutine(_routine);
        }

        private IEnumerator LoadingRoutine(float seconds)
        {
            yield return new WaitForSeconds(Const.DUR_FADEIN);

            float step = Time.deltaTime / seconds;
            while (_progress < 0.99f)
            {
                yield return null;

                if (_progress != _maxProgress)
                {
                    _progress = Mathf.Clamp(_progress + step, 0f, _maxProgress);
                    SetSlider(_progress);
                }
            }

            Animator.SetTrigger("FadeOut");
            yield return new WaitForSeconds(Const.DUR_FADEOUT);

            _routine = null;
            gameObject.SetActive(false);
        }

        public void SetMaxProgress(float maxProgress)
        {
            _maxProgress = Mathf.Clamp(maxProgress, 0f, 1f);
        }

        public void AddMaxProgress(float addProgress)
        {
            SetMaxProgress(_maxProgress + addProgress);
        }

        private void SetSlider(float value)
        {
            Slider.fillAmount = value;
            ProgressText.text = $"{value * 100:N0} %";
        }
    }
}

using Lean.Localization;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Nagih
{
    public class DataSelf : Singleton<DataSelf>
    {
        public UserData User { get; private set; }
        public DeviceData Device { get; private set; }
#if UNITY_EDITOR
        public TestUserData TestUser { get; set; }
#endif

        public GameTime Time { get; private set; }
        public LeanLocalization Localization { get; private set; }

        protected override void OnAwake()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        public void Initialize()
        {
            Localization = Instantiate(Resources.Load<GameObject>(Const.RESLOC_DATA_LEAN), transform).GetComponent<LeanLocalization>();
            LeanLocalization.UpdateTranslations();
        }

        public void LoadDataFromDevice()
        {
            User = Manager.GetInstance().PlayerPref.LoadTable<UserData>();
            Device = Manager.GetInstance().PlayerPref.LoadTable<DeviceData>();

            if (Device != null)
            {
                Localization.SetCurrentLanguage(Device.Language.ToString());
            }
        }

        public void CreateUser(string userId)
        {
            User = new UserData(userId);

            Manager.GetInstance().PlayerPref.SaveTable<UserData>(false);
            Debug.Log("CREATE NEW USER. " + JsonConvert.SerializeObject(User));

            Device = new DeviceData();
            Device.SetDefault();
            Manager.GetInstance().PlayerPref.SaveTable<DeviceData>();
            Manager.GetInstance().Audio.SyncVolume();

            Localization.SetCurrentLanguage(Device.Language.ToString());
        }

        public void CreateTime(long serverTime)
        {
            Time = new GameTime(serverTime);
        }

        public void SetUserDataFromServer(UserReturnData serverData)
        {
            User = serverData.Clone<UserReturnData, UserData>();
            Manager.GetInstance().PlayerPref.SaveTable<UserData>();
        }

        public void FinishTutorial()
        {
            User.FinishTutorial();
            Manager.GetInstance().PlayerPref.SaveTable<UserData>();

            UserRequestData reqData = new UserRequestSetting().SetTutorialStatus().Build();
            Manager.GetInstance().Request.Post<UserReturnData>(Const.USER_SET_PROFILE, reqData, null);
        }

        private void OnApplicationQuit()
        {
#if UNITY_EDITOR
            CancellationTokenSource cancelSource = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(new WaitCallback(CancelToken), cancelSource.Token);
            Thread.Sleep(1000);

            cancelSource.Cancel();
            Thread.Sleep(1000);
            cancelSource.Dispose();

            void CancelToken(object obj)
            {
                CancellationToken token = (CancellationToken)obj;

                try
                {
                    while (true)
                    {
                        if (token.IsCancellationRequested)
                        {
                            return;
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    Debug.Log("ASYNC CANCELED!");
                }
            }
#endif
        }
    }
}
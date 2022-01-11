using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Nagih
{
    public class DataStatic : Singleton<DataStatic>
    {
        public bool IsAndroidTv { get; private set; }
        public bool IsDeviceHasPlayServices { get; private set; }
#if ENABLE_INPUT_SYSTEM
        public InputActions InputActions { get; private set; }
#endif
        public GameDataSO GameDataSO { get; private set; }
        public Dictionary<Enum.Font, Font> FontData { get; private set; }
        public Dictionary<Enum.Icon, Sprite> IconSpriteData { get; private set; }

        public void Initialize()
        {
            IsDeviceHasPlayServices = Helper.IsPlayServicesAvailable();
            IsAndroidTv = Helper.IsAndroidTv();

#if ENABLE_INPUT_SYSTEM
            InputActions = new InputActions();
            InputActions.Enable();
#endif

            GameDataSO = Resources.Load<GameDataSO>(Const.RESLOC_DATA_GAMEINIT);
            GameDataSO.Initialize();

            FontData = Helper.BuildDictionaryTypesFromEnum<Enum.Font, Font>(x => $"Font/{x}");
            IconSpriteData = Helper.BuildDictionaryTypesFromEnum<Enum.Icon, Sprite>(x => $"Icon/{x}");
        }
    }
}
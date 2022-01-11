using UnityEngine;
using Newtonsoft.Json;

namespace Nagih
{
    public class UnitTestPlayerPref : MonoBehaviour
    {
#if UNITY_EDITOR
        [ContextMenu("Save")]
        public void SaveTestUserData()
        {
            DataSelf.GetInstance().TestUser = new TestUserData();
            Manager.GetInstance().PlayerPref.SaveTable<TestUserData>();
            Debug.Log("[PLAYERPREF] Save testUser: " + JsonConvert.SerializeObject(DataSelf.GetInstance().TestUser));
        }

        [ContextMenu("Load")]
        public void LoadTestUserData()
        {
            TestUserData testUser = Manager.GetInstance().PlayerPref.LoadTable<TestUserData>();
            Debug.Log("[PLAYERPREF] Load testUser: " + JsonConvert.SerializeObject(testUser));
        }
#endif
    }
}

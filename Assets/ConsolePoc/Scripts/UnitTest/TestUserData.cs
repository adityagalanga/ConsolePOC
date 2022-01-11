using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Nagih
{
    public class TestUserData
    {
        public static Dictionary<int, int> GetDefaultCountDictionary()
        //public static List<int> GetDefaultCountDictionary()
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            for (int i = 0; i < 5; i++)
            {
                dict[i] = (i + 1) * (i + 1);
            }

            //List<int> dict = new List<int>();
            //for (int i = 0; i < 3; i++)
            //{
            //    dict.Add((i + 1) * (i + 1));
            //}
            return dict;
        }

        public string UserId;
        public int TutorialStatus;
        public long LastPlaySeconds;
        public int NagihCoin;

        public Dictionary<int, int> CountDictionary;

        //public List<int> CountDictionary;

        public TestUserData()
        {
            UserId = "Test pertama nih";
            NagihCoin = 23231;
            CountDictionary = GetDefaultCountDictionary();
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Nagih
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameDataSO", order = 1)]
    public class GameDataSO : ScriptableObject
    {
        public SceneData[] SceneData;
        public float MinimumLoadingDuration = 2f;
        public AudioStruct[] AudioTypeData;
        public int[] TutorialSectionStep; // how many step in each section
        public int[] TutorialExcludeStepArray; // step apa saja yg tidak akan langsung pindah ke next step berikutnya jika click box, atau overlay

        public void Initialize()
        {

        }

        public Enum.AudioSource GetAudioType(Enum.Sound sound)
        {
            return AudioTypeData.First(x => x.Sounds.Contains(sound)).AudioSource;
        }

        [Serializable]
        public struct AudioStruct
        {
            public Enum.AudioSource AudioSource;
            public Enum.Sound[] Sounds;
        }
    }
}
using Newtonsoft.Json;

namespace Nagih
{
    public class UserData
    {
        public string UserId;
        public int TutorialStatus;
        public long LastPlaySeconds;
        public int NagihCoin;

        public UserData() { }

        public UserData(string id)
        {
            UserId = id;
            TutorialStatus = 0;
            LastPlaySeconds = 0;
            NagihCoin = 0;
        }

        public void FinishTutorial()
        {
            TutorialStatus = (int)Enum.TutorialStatus.Done;
        }

        [JsonIgnore]
        public bool HasDoneTutorial
        {
            get
            {
                return TutorialStatus == (int)Enum.TutorialStatus.Done;
            }
        }
    }
}
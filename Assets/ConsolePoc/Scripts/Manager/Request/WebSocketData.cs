using Newtonsoft.Json.Linq;

namespace Nagih
{
    public class WebSocketData
    {
        public int type;
        public JObject data;

        public WebSocketData() { }

        public WebSocketData(int number, JObject data)
        {
            type = number;
            this.data = data;
        }
    }

    public class WebSocketConnectionData
    {
        public int type;
        public IRequestData data;

        public WebSocketConnectionData(int type, IRequestData data)
        {
            this.type = type;
            this.data = data;
        }
    }
}

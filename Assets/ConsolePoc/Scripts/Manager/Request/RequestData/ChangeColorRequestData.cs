namespace Nagih
{
    public class ChangeColorRequestData : IRequestData
    {
        public string to_id;
        public string hex_color;

        public ChangeColorRequestData(string id,string color)
        {
            to_id = id;
            hex_color = color;
        }
    }
}
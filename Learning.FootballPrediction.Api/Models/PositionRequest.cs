namespace Learning.FootballPrediction.Api.Models
{
    public class Position
    {
        private byte _id;
        private string _name;

        public Position(byte id, string name)
        {
            this._id = id;
            this._name = name;
        }

        public byte ID => this._id;

        public string Name => this._name;        
    }
}
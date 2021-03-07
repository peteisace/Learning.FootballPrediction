namespace Learning.FootballPrediction.Api.Models
{
    public class ClubRequest
    {
        private string _name;
        private PlayerRequest[] _squad;

        public string Name
        {
            get => this._name;
            set => this._name = value;
        }

        public PlayerRequest[] Players
        {
            get => this._squad;
            set => this._squad = value;            
        }
    }
}
namespace Learning.FootballPrediction.Api.Models
{
    public class Club
    {
        private int _id;
        private string _name;

        private MatchdaySquad _squad;

        public Club(int id, string name)
        {
            this._id = id;
            this._name = name;
        }

        public int ID => this._id;
        
        public string Name  => this._name;        

        public MatchdaySquad Squad 
        {
            get => this._squad;
            set => this._squad = value;
        }
    }
}
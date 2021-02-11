namespace SheepWolfAndCabbage
{
    class Game
    {
        public bool GameOver
        {
            get { return checkGameOver(); }
        }
        public bool Win
        {
            get { return checkWin(); }
        }
        /// <summary>
        /// Possible positions.
        /// </summary>
        public enum Position { Start, End } 
        /// <summary>
        /// GameObjects that can be loaded or unloaded from the boat to the shore.
        /// </summary>
        public enum GameObjects { None, Sheep, Wolf, Cabbage }
        /// <summary>
        /// First shore
        /// </summary>
        /// <value>[0] = Sheep | [1] = Wold | [2] = Cabbage</value>
        public GameObjects[] Start { get; private set; }
         /// <summary>
        /// Second shore
        /// </summary>
        /// <value>[0] = Sheep | [1] = Wold | [2] = Cabbage</value>
        public GameObjects[] End { get; private set; }
        /// <summary>
        /// Boat's contents
        /// </summary>
        /// <value>What is inside the boat.</value>
        public GameObjects Boat { get; private set; }
        /// <summary>
        /// Boat's position
        /// </summary>
        /// <value>Where is the boat right now.</value>
        public Position BoatPos { get; private set; }

        /// <summary>
        /// Prepares the game and positions of the GameObjects.
        /// </summary>
        /// <param name="Sheep">Sheep's position.</param>
        /// <param name="Wolf">Wolf's position.</param>
        /// <param name="Cabbege">Cabbage's position.</param>
        public Game(Position Sheep, Position Wolf, Position Cabbege)
        {
            this.BoatPos = Position.Start;
            this.Start = new GameObjects[3];
            this.End = new GameObjects[3];

            if (Sheep == Position.Start) this.Start[0] = GameObjects.Sheep;
            else this.End[0] = GameObjects.Sheep;
            if (Wolf == Position.Start) this.Start[1] = GameObjects.Wolf;
            else this.End[1] = GameObjects.Wolf;
            if (Cabbege == Position.Start) this.Start[2] = GameObjects.Cabbage;
            else this.End[2] = GameObjects.Cabbage;
        }


        /// <summary>
        /// Loads selected GameObject to the boat.
        /// </summary>
        /// <param name="toLoad">Desired GameObject to be loaded.</param>
        public void LoadOnShip(GameObjects toLoad)
        {
            if (Boat == toLoad) return;
            if (BoatPos == Position.Start && Start[(int)toLoad - 1] != GameObjects.None)
            {
                if (Boat != GameObjects.None) Start[(int)Boat - 1] = Boat;
                Boat = Start[(int)toLoad - 1];
                Start[(int)toLoad - 1] = GameObjects.None;
                return;
            }
            if (BoatPos == Position.End && End[(int)toLoad - 1] != GameObjects.None)
            {
                if (Boat != GameObjects.None) End[(int)Boat - 1] = Boat;
                Boat = End[(int)toLoad - 1];
                End[(int)toLoad - 1] = GameObjects.None;
                return;
            }
        }
        /// <summary>
        /// Unloads the the GameObject, that is inside the boat, to the side that it's on.
        /// </summary>
        public void UnloadFromShip()
        {
            if (Boat != GameObjects.None)
            {
                if (BoatPos == Position.Start) Start[(int)Boat - 1] = Boat;
                if (BoatPos == Position.End) End[(int)Boat - 1] = Boat;
                Boat = GameObjects.None;
            }
        }
        /// <summary>
        /// Moves the boat form one side to the other.
        /// </summary>
        public void Move()
        {
            if (BoatPos == Position.Start) BoatPos = Position.End;
            else BoatPos = Position.Start;
        }
        /// <summary>
        /// Checks if the player failed(Cabbage or Sheep gets eaten).
        /// </summary>
        /// <returns>True if player failed.</returns>
        private bool checkGameOver()
        {
            if (End[0] != GameObjects.None && End[1] != GameObjects.None && BoatPos == Position.Start) return true;
            if (End[0] != GameObjects.None && End[2] != GameObjects.None && BoatPos == Position.Start) return true;
            if (Start[0] != GameObjects.None && Start[1] != GameObjects.None && BoatPos == Position.End) return true;
            if (Start[0] != GameObjects.None && Start[2] != GameObjects.None && BoatPos == Position.End) return true;
            return false;
        }

        private bool checkWin()
        {
            if (End[0] == GameObjects.Sheep && End[1] == GameObjects.Wolf && End[2] == GameObjects.Cabbage) return true;
            return false;
        }
        /// <summary>
        /// AIDS method for what you shoudl be doing.
        /// </summary>
        /// <returns>Gives you step by step what you should do to achive victory.</returns>
        public string GetHint() //Unfortunately there is no other better way for this. With the data types that I using. 
        {
            if (End[0] == GameObjects.None && End[1] == GameObjects.None && End[2] == GameObjects.None)
            {
                if (BoatPos == Position.Start && Boat == GameObjects.None) return "Load the SHEEP";
                else if (BoatPos == Position.Start && Boat == GameObjects.Sheep) return "Get SHEEP to the other side";
                else if (BoatPos == Position.End && Boat == GameObjects.Sheep) return "Unload the SHEEP";
            }
            else if (End[0] == GameObjects.Sheep && End[1] == GameObjects.None && End[2] == GameObjects.None)
            {
                if (BoatPos == Position.End && Boat == GameObjects.None) return "Go to the other side";
                else if (BoatPos == Position.Start && Boat == GameObjects.None) return "Load the CABBAGE or WOLF";
                else if (BoatPos == Position.Start && Boat == GameObjects.Cabbage) return "Get the CABBAGE to the other side";
                else if (BoatPos == Position.End && Boat == GameObjects.Cabbage) return "Unload the CABBAGE";
                else if (BoatPos == Position.Start && Boat == GameObjects.Wolf) return "Get the WOLF to the other side";
                else if (BoatPos == Position.End && Boat == GameObjects.Wolf) return "Unload the WOLF";
            }
            else if (End[0] == GameObjects.Sheep && ((End[1] == GameObjects.None && End[2] == GameObjects.Cabbage) || (End[1] == GameObjects.Wolf && End[2] == GameObjects.None)))
            {
                if (BoatPos == Position.End && Boat == GameObjects.None) return "Load the SHEEP";
            }
            else if (End[0] == GameObjects.None && ((End[1] == GameObjects.None && End[2] == GameObjects.Cabbage) || (End[1] == GameObjects.Wolf && End[2] == GameObjects.None)))
            {
                if (BoatPos == Position.End && Boat == GameObjects.Sheep) return "Get SHEEP to the other side";
                else if (BoatPos == Position.Start && Boat == GameObjects.Sheep) return "Unload the SHEEP";
                else if (BoatPos == Position.Start && Boat == GameObjects.None) return "Load the CABBAGE";
                else if (BoatPos == Position.Start && Boat == GameObjects.Cabbage) return "Go to the other side";
                else if (BoatPos == Position.End && Boat == GameObjects.Cabbage) return "Unload the CABBAGE";
                else if (BoatPos == Position.Start && Boat == GameObjects.None) return "Load the WOLF";
                else if (BoatPos == Position.Start && Boat == GameObjects.Wolf) return "Go to the other side";
                else if (BoatPos == Position.End && Boat == GameObjects.Wolf) return "Unload the WOLF";
            }
            else if (End[0] == GameObjects.None && End[1] == GameObjects.Wolf && End[2] == GameObjects.Cabbage)
            {
                if (BoatPos == Position.End && Boat == GameObjects.None) return "Go to the other side";
                else if (BoatPos == Position.Start && Boat == GameObjects.None) return "Load the SHEEP";
                else if (BoatPos == Position.Start && Boat == GameObjects.Sheep) return "Go to the other side";
                else if (BoatPos == Position.End && Boat == GameObjects.Sheep) return "Unload the SHEEP an you win :)";
            }
            return null;
        }
    }




}
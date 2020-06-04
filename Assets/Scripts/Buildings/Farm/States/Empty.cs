namespace Buildings.Farm.States
{
    public class Empty : FarmingState
    {
        private static Empty instance;

        public static Empty Instance
        {
            get
            {
                if (instance == null) { instance = new Empty(); }

                return instance;
            }
        }

        private Empty() { }
    }
}
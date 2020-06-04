namespace Buildings.Farm.States
{
    public abstract class FarmingState
    {
        public virtual void Start(Farm owner) { }

        public virtual void Execute(Farm owner) { }

        public virtual void Stop(Farm owner) { }
    }
}
namespace Shiang
{
    public abstract class StateManager
    {
        StateMachine _sm;
        IGameEntity _entity;

        public StateManager() => _sm = new StateMachine();

        protected StateMachine SM { get => _sm; }
        protected IGameEntity Owner { get => _entity; }

        public void SetOwner(IGameEntity entity) => _entity = entity;

        public abstract void InitStates();

        public abstract void InitTransitions();

        public abstract void SetInitialState();

        public void Tick() => _sm.Tick();
    }
}
using Cysharp.Threading.Tasks;

public interface IGameInitiator
{

    public abstract void Start();

    public abstract UniTask CreateObjects();

    public abstract UniTask InitializeObjects();

    public abstract void BindObjects();
}

using Cysharp.Threading.Tasks;

public interface IGameInitiator
{

    abstract void Start();

    abstract UniTask CreateObjects();

    abstract UniTask InitializeObjects();

    abstract void BindObjects();
}

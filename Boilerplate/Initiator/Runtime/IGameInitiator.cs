using Cysharp.Threading.Tasks;

namespace Initiator.Runtime
{
    public interface IGameInitiator
    {

        abstract void Start();

        abstract UniTask CreateObjects();

        abstract UniTask InitializeObjects();

        abstract void BindObjects();
    }

}

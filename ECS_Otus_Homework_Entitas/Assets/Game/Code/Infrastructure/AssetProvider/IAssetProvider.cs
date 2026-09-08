using UnityEngine;

namespace Infrastructure
{
    public interface IAssetProvider
    {
        T Load<T>(string path) where T : Object;
    }
}
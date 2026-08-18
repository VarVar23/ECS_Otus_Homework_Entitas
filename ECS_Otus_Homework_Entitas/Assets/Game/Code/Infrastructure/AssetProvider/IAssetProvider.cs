using UnityEngine;

namespace Infrastructure
{
    public interface IAssetProvider
    {
        GameObject Load(string path);
    }
}
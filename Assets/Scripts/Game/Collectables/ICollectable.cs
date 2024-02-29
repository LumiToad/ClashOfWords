using UnityEngine;

namespace Clash_Of_Words.Collectable
{
    public interface ICollectable
    {
        public void Collect(Player player);

        public void DeleteCollectable();
    }
}
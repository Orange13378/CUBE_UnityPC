using System.Collections.Generic;

namespace CubeECS
{
    public struct ChestComponent
    {
        public List<ChestItem> Items;

        public delegate void OnItemInteracted();

        public OnItemInteracted OnItemInteractedCallback;
    }
}
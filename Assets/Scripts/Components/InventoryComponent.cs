using System.Collections.Generic;

namespace CubeECS
{
    public struct InventoryComponent
    {
        public List<Item> Items;

        public delegate void OnItemChanged();
        public OnItemChanged OnItemChangedUICallback;
        public OnItemChanged OnItemChangedCallback;
    }
}
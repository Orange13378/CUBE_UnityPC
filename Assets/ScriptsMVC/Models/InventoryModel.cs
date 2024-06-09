using System.Collections.Generic;

namespace CubeMVC
{
    public class InventoryModel
    {
        public List<Item> Items = new();

        public delegate void OnItemChanged();
        public OnItemChanged OnItemChangedUICallback;
        public OnItemChanged OnItemChangedCallback;
    }
}
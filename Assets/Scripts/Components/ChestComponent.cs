using System.Collections.Generic;
using UnityEngine;

namespace CubeECS
{
    public struct ChestComponent
    {
        public List<ChestItem> Items;
        public GameObject CurrentOpenedItem;

        public delegate void OnItemInteracted();
        public OnItemInteracted OnItemInteractedCallback;
    }
}
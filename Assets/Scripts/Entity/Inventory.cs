using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    public class Inventory : MonoBehaviour
    {
        private HashSet<IInventoryItem> _items;

        /// <summary>
        /// Items in inventory
        /// </summary>
        public IEnumerable<IInventoryItem> Items => this._items;

        public int capacity = 1;

        /// <summary>
        /// Adds item if not already present AND not at capacity
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <returns>Whether item was added</returns>
        public bool TryAddItem(IInventoryItem item)
        {
            if (this._items.Count >= this.capacity || this._items.Contains(item)) { return false; }

            this._items.Add(item);

            return true;
        }

        private void Start()
        {
            this._items = new HashSet<IInventoryItem>();
        }
    }
}
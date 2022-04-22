namespace MultiplayerPokemon.Shared.Logic
{
    public static class PartyLogicHelper
    {
        public static bool AddToCollection<T>(this IDictionary<int, T> collection, T item)
        {
            if (collection.Count < 6)
            {
                collection.Add(collection.Count, item);
                return true;
            }

            return false;
        }

        public static bool RemoveFromCollection<T>(this IDictionary<int, T> collection, int position)
        {
            if (collection.ContainsKey(position))
            {
                for (int i = position; i < collection.Count - 1; i++)
                {
                    collection[i] = collection[i + 1];
                }

                return collection.Remove(collection.Count - 1);
            }

            return false;
        }

        public static bool RemoveMultipleFromCollection<T>(this IDictionary<int, T> collection, IEnumerable<int> positions)
        {
            bool allRemoved = true;
            foreach (int position in positions.OrderByDescending(x => x))
            {
                if (collection.ContainsKey(position))
                {
                    for (int i = position; i < collection.Count - 1; i++)
                    {
                        collection[i] = collection[i + 1];
                    }

                    collection.Remove(collection.Count - 1);
                }
                else
                {
                    allRemoved = false;
                }
            }

            return allRemoved;
        }

        public static bool Swap<T>(this IDictionary<int, T> collection, int currentPosition, int newPosition)
        {
            if (collection.ContainsKey(currentPosition) && collection.ContainsKey(newPosition))
            {
                var pokemonToSwap = collection[currentPosition];
                var pokemonInPreviousSlot = collection[newPosition];
                collection[currentPosition] = pokemonInPreviousSlot;
                collection[newPosition] = pokemonToSwap;
                return true;
            }

            return false;
        }
    }
}

using System.Collections.Generic;

namespace MultiValueDictionary
{
    public interface IGenericMultiValueDictionary<TKey, TValue>
    {
        /// <summary>
        /// Adds a member to a collection for a given key. Displays an error if the member already exists for the key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        bool Add(TKey key, TValue val);

        /// <summary>
        /// Removes all keys and all members from the dictionary.
        /// </summary>
        /// <returns></returns>
        bool ClearAll();


        /// <summary>
        /// Display members related to dictionary
        /// </summary>
        /// <param name="values"></param>
        void DisplayMembers(List<TValue> values);

        /// <summary>
        /// Returns all the members in the dictionary. Returns nothing if there are none. Order is not guaranteed.
        /// </summary>
        /// <returns></returns>
        List<TValue> GetAllMembers();

        /// <summary>
        /// Returns all keys in the dictionary and all of their members. 
        /// Returns nothing if there are none. Order is not guaranteed.
        /// </summary>
        void GetItems();

        /// <summary>
        /// Returns all the keys in the dictionary. Order is not guaranteed.
        /// </summary>
        /// <returns></returns>
        List<TKey> GetKeys();

        /// <summary>
        /// Returns the collection of strings for the given key.
        /// Return order is not guaranteed. Returns an error if the key does not exists
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        List<TValue> GetMembers(TKey key);

        /// <summary>
        /// Returns whether a key exists or not.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool KeyExists(TKey key);

        /// <summary>
        /// Returns whether a member exists within a key. Returns false if the key does not exist.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        bool MemberExists(TKey key, TValue val);

        /// <summary>
        /// Removes all members for a key and removes the key from the dictionary.
        /// Returns an error if the key does not exist.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool RemoveKey(TKey key);

        /// <summary>
        /// Removes a member from a key. If the last member is removed from the key, the key is removed from the dictionary.
        /// If the key or member does not exist, displays an error.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        bool RemoveMember(TKey key, TValue val);
    }
}
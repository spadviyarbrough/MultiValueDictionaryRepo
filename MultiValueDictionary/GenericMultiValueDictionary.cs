using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiValueDictionary
{
    public class GenericMultiValueDictionary<TKey, TValue> : Dictionary<TKey, List<TValue>>, IGenericMultiValueDictionary<TKey, TValue>
    {
        private readonly ILogger _logger;
        public GenericMultiValueDictionary(ILogger<GenericMultiValueDictionary<TKey, TValue>> logger) : base()
        {
            _logger = logger;
        }

        public bool Add(TKey key, TValue val)
        {
            try
            {
                List<TValue> list;

                if (TryGetValue(key, out list))
                {
                    if (list.Contains(val))
                    {
                        throw new Exception("ERROR, member already exists for key");

                    }
                    else
                    {
                        list.Add(val);
                        Console.WriteLine(") Added");
                        return true;
                    }

                }
                else
                {
                    list = new List<TValue>();
                    list.Add(val);
                    Add(key, list);
                    Console.WriteLine(") Added");
                    return true;
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(") "+ex.Message);
                _logger.LogInformation(ex, "GenericMultiValueDictionary.Add");
                return false;
            }
        }

        public List<TValue> GetMembers(TKey key)
        {
            try
            {
                List<TValue> valueList;

                if (TryGetValue(key, out valueList))
                {
                    return valueList;
                }

                else
                {
                    throw new Exception("ERROR,  key does not exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(") "+ex.Message);
                _logger.LogInformation(ex, "GenericMultiValueDictionary.GetMembers");
                return new List<TValue>();
            }
        }

        public List<TKey> GetKeys()
        {
            try
            {
                Dictionary<TKey, List<TValue>>.KeyCollection keys = Keys;
                int i = 1;
                foreach (var key in Keys)
                {
                    Console.WriteLine($"{i}) {key}");
                    i++;
                }

                if (this.Keys.Count == 0)
                    Console.WriteLine(") empty set");


                return new List<TKey>(Keys);

            }
            catch (Exception ex)
            {
                Console.WriteLine(") "+ex.Message);
                _logger.LogInformation(ex, "GenericMultiValueDictionary.GetKeys");
                return new List<TKey>();
            }

        }

        public bool RemoveMember(TKey key, TValue val)
        {
            try
            {
                List<TValue> valueList;

                if (TryGetValue(key, out valueList))
                {
                    if (valueList.Contains(val))
                        valueList.Remove(val);
                    else
                        throw new Exception("ERROR, member does not exist");

                }
                else
                    throw new Exception("ERROR,  key does not exist.");

                if (valueList.Count == 0)
                    this.Remove(key);

                Console.WriteLine(") Removed");

                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(") "+ex.Message);
                _logger.LogInformation(ex, "GenericMultiValueDictionary.RemoveMember");
                return false;
            }
        }

        public bool RemoveKey(TKey key)
        {
            try
            {
                if (KeyExists(key))
                    this.Remove(key);
                else
                {
                    throw new Exception("ERROR, key does not exist.");
                }

                Console.WriteLine(") Removed");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(") "+ex.Message);
                _logger.LogInformation(ex, "GenericMultiValueDictionary.RemoveKey");
                return false;
            }
        }

        public bool ClearAll()
        {
            try
            {
                this.Clear();
                Console.WriteLine(") Cleared");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(") "+ex.Message);
                _logger.LogInformation(ex, "GenericMultiValueDictionary.ClearAll");
                return false;
            }
        }

        public bool KeyExists(TKey key)
        {
            try
            {
                bool exists = false;
                if (this.ContainsKey(key))
                    exists = true;

                return exists;
            }
            catch (Exception ex)
            {
                Console.WriteLine(") "+ex.Message);
                _logger.LogInformation(ex, "GenericMultiValueDictionary.KeyExists");
                return false;
            }
        }

        public bool MemberExists(TKey key, TValue val)
        {
            try
            {
                bool exists = false;
                if (KeyExists(key))
                {
                    var values = this[key];
                    if (values.Contains(val))
                        exists = true;
                    else
                        exists = false;
                }
                else
                {
                    exists = false;
                }

                return exists;
            }
            catch (Exception ex)
            {
                Console.WriteLine(") "+ex.Message);
                _logger.LogInformation(ex, "GenericMultiValueDictionary.MemberExists");
                return false;
            }
        }

        public List<TValue> GetAllMembers()
        {
            try
            {
                List<TValue> members = new List<TValue>();

                foreach (var key in this.Keys)
                {
                    members.AddRange(GetMembers(key));
                }

                if (members.Count == 0)
                    Console.WriteLine("(empty set)");


                return members;
            }
            catch (Exception ex)
            {
                Console.WriteLine(") "+ex.Message);
                _logger.LogInformation(ex, "GenericMultiValueDictionary.GetAllMembers");
                return new List<TValue>();
            }
        }

        public void DisplayMembers(List<TValue> values)
        {
            int i = 1;

            foreach (var val in values)
            {
                Console.WriteLine(i.ToString() + ") " + val);
                i++;
            }
        }

        public void GetItems()
        {
            if (this.Count == 0)
                Console.WriteLine("(empty set)");

            int i = 1;
            foreach (var entry in this)
            {
                var values = entry.Value;

                foreach (var val in values)
                {
                    Console.WriteLine(i + ") " + entry.Key + ":" + val);
                    i++;
                }
            }

        }
    }
}

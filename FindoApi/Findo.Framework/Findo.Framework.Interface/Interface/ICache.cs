using System;
using System.Collections.Generic;

namespace Findo.Framework.Interface.Interface
{
    public interface ICache
    {
        TItem GetOrCreate<TItem>(string key, Func<TItem> func);

        List<TItem> GetOrCreate<TItem>(string key, Func<List<TItem>> func);

        void ExpireKeysByGuid(Guid guid);

        void AddKeysRelationShip(string key, List<Guid> listaIdsRelation);
    }
}

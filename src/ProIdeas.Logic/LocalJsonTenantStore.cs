﻿using ProIdeas.Logic.Contracts;
using ProIdeas.Files.Contracts;
using ProIdeas.Serializers.Contracts;
using ProIdeas.Domain.Entities;

namespace ProIdeas.Logic
{
    public class LocalJsonTenantStore : ITenantStore
    {
        private readonly IFileStorage _storageProvider;
        private readonly IJsonSerializer _jsonSerializer;
        public LocalJsonTenantStore(IFileStorage storageProvider, IJsonSerializer jsonSerializer)
        {
            _storageProvider = storageProvider;
            _jsonSerializer = jsonSerializer;
        }

        public TenantSettings GetTenant(string uniqueKey)
        {

            //using (var streamReader = new StreamReader(_storageProvider.GetFileStream("tenants.json")))
            //{
            //   var data = _jsonSerializer.Deserialize<IEnumerable<TenantSettings>>(streamReader.ReadToEnd());

            //   return  data.FirstOrDefault(i => i.Id == uniqueKey);
            //}

            return default(TenantSettings);

                
        }
    }
}

using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaundryIroningContract.Infrastructure
{
    public interface IPatchService<TEntity> where TEntity : class
    {
        Task<int> Patch(Guid key, JsonPatchDocument<TEntity> data);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakeApi.Dtos;
using TakeApi.Model;

namespace TakeApi.Data
{
    public interface IService
    {
        Task<List<Challenge>> GetRepositories();
    }
}

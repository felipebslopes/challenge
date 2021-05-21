
using AutoMapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TakeApi.Data;
using TakeApi.Dtos;
using TakeApi.Model;
using TakeApi.Settings;

namespace TakeApi.Data
{
    public class Service : IService
    {
        private IMapper _mapper;
        private readonly IOptions<TakeApiSettings> _takeApiSettings;
        public Service(IMapper mapper, IOptions<TakeApiSettings> takeApiSettings)
        {
            _mapper = mapper;
            _takeApiSettings = takeApiSettings;
        }
        //Lista todos os repositórios que estão no git
        public async Task<List<Challenge>> GetRepositories()
        {
            List<RepositoriesDTO> challengesDTO = new List<RepositoriesDTO>();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", "application/vnd.github.v3+json");
                using (var response = await httpClient.GetAsync(_takeApiSettings.Value.UrlGit))
                {
                    if (response.IsSuccessStatusCode)
                    {

                        var apiResponse = JsonConvert.DeserializeObject<List<RepositoriesDTO>>(await response.Content.ReadAsStringAsync());
                        challengesDTO = FiltroChallenge(apiResponse);
                    }
                }
            }
            var challenges = RepostoriesToChallenges(challengesDTO);

            return challenges;


        }

        //Filtra os repositórios de acordo com a definição do teste
        private List<RepositoriesDTO> FiltroChallenge(List<RepositoriesDTO> repos)
        {
            var repositories = repos.Where(x => x.language != null).Where(repos => repos.language.Contains("C#"))
                                                                                    .OrderBy(x => Convert.ToDateTime(x.created_at)).Take(5).ToList();

            var reposIDIncrement = SetIDIncrement(repositories);
            return reposIDIncrement;
        }

        //Mapeia os campos que preciso para retornar para o ChatBot
        private List<Challenge> RepostoriesToChallenges(List<RepositoriesDTO> repositories)
        {
            var challenges = _mapper.Map<IEnumerable<Challenge>>(repositories).ToList();
            return challenges;
        }

        //Seta id incremental para que o bot consiga entender a posição de cada repositório
        private List<RepositoriesDTO> SetIDIncrement(List<RepositoriesDTO> repos)
        {
            var count = 0;
            foreach (var item in repos)
            {
                item.id = count.ToString();
                count++;
            }

            return repos;


        }
    }
}

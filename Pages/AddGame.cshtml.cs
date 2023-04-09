using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using VideoGameCasus.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace VideoGameCasus.Pages
{
    class ApiGame
    {
        public int id { get; set; }
        public List<int> age_ratings { get; set; }
        public double aggregated_rating { get; set; }
        public int aggregated_rating_count { get; set; }
        public List<int> alternative_names { get; set; }
        public List<int> artworks { get; set; }
        public List<int> bundles { get; set; }
        public int category { get; set; }
        public int collection { get; set; }
        public int cover { get; set; }
        public int created_at { get; set; }
        public List<int> external_games { get; set; }
        public int first_release_date { get; set; }
        public int follows { get; set; }
        public List<int> game_engines { get; set; }
        public List<int> game_modes { get; set; }
        public List<int> genres { get; set; }
        public List<int> involved_companies { get; set; }
        public List<int> keywords { get; set; }
        public List<int> multiplayer_modes { get; set; }
        public string name { get; set; }
        public List<int> platforms { get; set; }
        public List<int> player_perspectives { get; set; }
        public double rating { get; set; }
        public int rating_count { get; set; }
        public List<int> release_dates { get; set; }
        public List<int> screenshots { get; set; }
        public List<int> similar_games { get; set; }
        public string slug { get; set; }
        public string storyline { get; set; }
        public string summary { get; set; }
        public List<int> tags { get; set; }
        public List<int> themes { get; set; }
        public double total_rating { get; set; }
        public int total_rating_count { get; set; }
        public int updated_at { get; set; }
        public string url { get; set; }
        public List<int> videos { get; set; }
        public List<int> websites { get; set; }
        public string checksum { get; set; }
        public List<int> language_supports { get; set; }
        public List<int> game_localizations { get; set; }
        public int? parent_game { get; set; }
        public int? status { get; set; }
        public List<int> franchises { get; set; }
    }

    class Company
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    class InvolvedCompany
    {
        public int id { get; set; }
        public Company company { get; set; }
    }

    class CompanyRoot
    {
        public int id { get; set; }
        public List<InvolvedCompany> involved_companies { get; set; }
    }

    class IGDBApi
    {
        private const string BaseUrl = "https://api.igdb.com/v4/";
        private readonly string _apiKey;

        public IGDBApi(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<List<ApiGame>> SearchGames(string query)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, BaseUrl + "games");
            request.Headers.Add("Client-ID", "9q14bvj25yomgp5jc7u2ouor0yc37m");
            request.Headers.Add("Authorization", $"Bearer {_apiKey}");
            request.Content = new StringContent($"search \"{query}\"; fields *;", System.Text.Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<ApiGame>>(content);
        }

        public async Task<string> GetPublisher(int id)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, BaseUrl + "games");
            request.Headers.Add("Client-ID", "9q14bvj25yomgp5jc7u2ouor0yc37m");
            request.Headers.Add("Authorization", $"Bearer {_apiKey}");
            request.Content = new StringContent($"fields involved_companies.company.name; where id = {id};", System.Text.Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<List<CompanyRoot>>(content);

            return results[0].involved_companies[0].company.name;
        }

        public string GetReleaseDate(int releaseDate)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(releaseDate);
            DateTime dateTime = dateTimeOffset.UtcDateTime;
            return dateTime.ToLongDateString();
        }

        public string GetGenre(int genreId)
        {
            return "TODO";
        }

        public string GetPlatform(int platformId)
        {
            return "TODO";
        }

        public string GetCover(int coverId)
        {
            return "TODO";
        }
    }

    public class AddGameModel : PageModel
    {
        [BindProperty]
        public string IgdbSearchString { get; set; }
        public Game newGame { get; set; }
        public int GameListId { get; set; }
        public bool ApiSyncDone;

        public void OnGet(int gameListId, bool apiSyncDone)
        {
            GameListId = gameListId;
            ApiSyncDone = apiSyncDone;
        }

        public async Task<IActionResult> OnPost(int gameListId, bool apiSyncDone)
        {
			GameListId = gameListId;
			ApiSyncDone = apiSyncDone;

			if (ApiSyncDone) // User heeft API geraadpleegt. Voeg game toe o.b.v. ingevulde gegevens
            {
                return RedirectToPage("/Index");
            }
            else
            {
                string apiKey = "myjqcshxoe9sgezrly5auzyujzh0kl";
                var api = new IGDBApi(apiKey);
                List<ApiGame> games = await api.SearchGames(IgdbSearchString);
                
                if (games.Count == 0)
                {
                    // Geen game gevonden
                    return RedirectToPage("/Index");
                }

                newGame = new Game
                {
                    Name = games[0].name,
                    Summary = games[0].summary,
                    Publisher = await api.GetPublisher(games[0].id),
                    ReleaseDate = api.GetReleaseDate(games[0].first_release_date),
                    Genre = api.GetGenre(games[0].genres[0]),
                    Platform = api.GetPlatform(games[0].platforms[0]),
                    Cover = api.GetCover(games[0].cover),
                    Finished = false,
                    GameListId = gameListId
                };

                ApiSyncDone = true;
                return Page();
            }
        }
    }
}

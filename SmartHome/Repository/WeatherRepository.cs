

using AngleSharp;

namespace SmartHome.Repository
{
    public interface IWeatherRepository
    {
       Task<string> GetTemperature();
    }
    public class WeatherRepository : IWeatherRepository
    {
        string url = "https://www.ilmeteo.it/meteo/Padova/amp";
        public async Task<string> GetTemperature()
        {
            string temp = string.Empty;

            var data = await GetHtml(url)
                .ContinueWith(c =>  GetContent(c.Result).Result);

            foreach (var item in data.Take(1))
            {
               temp = item.TextContent;
            }
            return temp.Split(" ").Last();
        }


        //TODO: Refactoring => create a scrapper add methods to it
        public async Task<string> GetHtml(string url)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<List<AngleSharp.Dom.IElement>> GetContent(string html)
        {

            var config = Configuration.Default;
            using var context = BrowsingContext.New(config);
            using var doc = await context.OpenAsync(req => req.Content(html));


            return  doc.QuerySelectorAll("*")
                   .Where(e => e.LocalName == "div" && e.ClassName == "data-row")
                   .ToList();
        }
    }
}

using AngleSharp;
using AngleSharp.Dom;
using SmartHome.DTOs.Weather;

namespace SmartHome.Repository
{
    public interface IWeatherRepository
    {
        Task<string> GetTemperature();
        Task<IEnumerable<WeekWeatherDto>> GetWeeklyWeatherReport();
    }
    public class WeatherRepository : IWeatherRepository
    {
        public async Task<string> GetTemperature()
        {
            string url = "https://www.ilmeteo.it/meteo/Padova/amp";
            string temp = string.Empty;

            var data = await GetHtml(url)
                .ContinueWith(c => GetContent(c.Result).Result);

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


            return doc.QuerySelectorAll("*")
                   .Where(e => e.LocalName == "div" && e.ClassName == "data-row")
                   .ToList();
        }


        public async Task<IEnumerable<WeekWeatherDto>> GetWeeklyWeatherReport()
        {
            string url = "https://www.ilmeteo.it/meteo/San+Pietro+Viminario";

            var data = await GetHtml(url)
                .ContinueWith(htmlString => ExecuteQuery(htmlString.Result, "ul", "daytabs")
                .Result);

            var dataList = data[0].TextContent.Split("\n").Skip(1).Take(7);

            return Mapper(dataList);
        }

        private async Task<List<IElement>> ExecuteQuery(string html, string element, string name)
        {
            var config = Configuration.Default;
            using var context = BrowsingContext.New(config);
            using var doc = await context.OpenAsync(req => req.Content(html));

            return doc?.QuerySelectorAll("*")
                   .Where(e => e.LocalName == element && e.Id == name)
                   .ToList();
        }

        private List<WeekWeatherDto> Mapper(IEnumerable<string> data)
        {
            var dataList = new List<WeekWeatherDto>();

            foreach (var item in data)
            {
                dataList.Add(new WeekWeatherDto
                {
                    Day = item.Split(" ").Take(1).First(),
                    Date = item.Split(" ").Skip(1).Take(1).First(),
                    RangeFrom = item.Split(" ").Skip(2).Take(1).First().Split("°").First(),
                    RangeTo = item.Split(" ").Skip(2).Take(1).First().Split("°").Skip(1).First()
                });
            }
            return dataList;
        }
    }
}

using System.Web;

namespace DemoApp.Helpers
{
    public static class QueryStringHelper
    {
        public static string BuildUrlWithQueryStringUsingStringConcat(
    string basePath, Dictionary<string, string> queryParams)
        {
            var queryString = string.Join("&",
                queryParams.Select(kvp => $"{HttpUtility.UrlEncode(kvp.Key)}={HttpUtility.UrlEncode(kvp.Value)}"));

            return $"{basePath}?{queryString}";
        }

        public static Dictionary<string, string> ConvertFromObjectToDictionary(object arg)
        {
            return arg.GetType().GetProperties().ToDictionary(property => property.Name, property => property.GetValue(arg).ToString());
        }
    }
}

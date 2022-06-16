public class Request
{
    private string user_agent { get; set; }
    private string cookie { get; set; }
    private string url { get; set; }
    private string platform { get; set; }
    private string provider { get; set; }
    private string os { get; set; }
    private string version { get; set; }
    private Dictionnary<string, string> platforms = new Dictionnary<string, string>()
    {
        { "steam","steam" },
        { "epic", "brill" },
        { "microsoft", "grdk" }
    };

    public void update(string user_agent, string cookie, string url, string platform)
    {
        this.user_agent = user_agent;
        this.cookie = cookie;
        this.url = url;
        this.platform = platforms.platform
    }

    private HttpWebRequest build()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

        request.Method = "POST";
        request.ContentType = "application/octet-stream";
        request.UserAgent = user_agent;
        request.Timeout = 5000;
        request.ServicePoint.Expect100Continue = false;
        request.Headers.Add("Cookie", cookie);
        request.Headers.Add("x-kraken-client-version", version);
        request.Headers.Add("x-kraken-client-provider", provider);
        request.Headers.Add("x-kraken-client-platform", client);
        request.Headers.Add("x-kraken-client-os", os);

        return (request);
    }

    public string send()
    {
        HttpWebRequest request = build();

        using (Stream requestStream = request.GetRequestStream())
        {
            byte[] requestAsByteArray = System.Text.Encoding.UTF8.GetBytes(content);
            requestStream.Write(requestAsByteArray, 0, requestAsByteArray.Length);
        }
        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        using (Stream responseStream = response.GetResponseStream())
        using (StreamReader reader = new StreamReader(responseStream))
        {
            return reader.ReadToEnd();
        }
    }
}
// See https://aka.ms/new-console-template for more information
using System.Text;
using Newtonsoft.Json;

if (args.Length > 0)
{
    HttpClient client = new HttpClient();
    client.DefaultRequestHeaders.Add("authorization","Bearer Your API Key here");

    var content = new StringContent ("{\"model\": \"text-davinci-001\", \"prompt\": \""+ args[0] +"\",\"temperature\": 1,\"max_tokens\": 100}",Encoding.UTF8, "application/json");

    HttpResponseMessage respomse = await client.PostAsync("https://api.openai.com/v1/completions", content);

    string responsestring = await respomse.Content.ReadAsStringAsync();

    //Console.WriteLine(responsestring);

    try
    {
        var data = JsonConvert.DeserializeObject<Root>(responsestring);
        
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Hello AI Answered:");
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(data.choices[0].text);

        Console.ResetColor();

    }

    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

else
{
    Console.WriteLine("You must write something");
}
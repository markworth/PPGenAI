using Azure.Core;
using Newtonsoft.Json;
using OpenAI.Chat;

namespace GPT.Request
{
    public class GPTRequest
    {
        public string model { get; set; }
        public List<ChatMessage> messages { get; set; }
        public double temperature { get; set; }

        public async Task<string> Run()
        {
            var result = await GPT4o.GetAI(this);
            var text = result?.Content[0].Text;
            return text;
        }

        public async Task<T> Run<T>()
        {
            var success = false;
            int count = 0;

            while (!success && count++ < 10)
            {
                try
                {
                    var result = await GPT4o.GetAI(this);
                    var text = result?.Content[0].Text;
                    var deser = JsonConvert.DeserializeObject<T>(text);

                    if (deser != null)
                    {
                        File.WriteAllText("./output.json", JsonConvert.SerializeObject(deser));
                        Console.WriteLine(JsonConvert.SerializeObject(deser));
                        success = true;
                        return deser;
                    }
                }
                catch
                {
                    Console.WriteLine("FAILURE!");
                }
            };

            throw new Exception("FAILED");
        }
    }
}

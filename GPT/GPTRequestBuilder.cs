using GPT.Request;
using OpenAI.Chat;

namespace GPT
{
    public interface IGPTRequestPersona
    {
        IGPTRequestBuilder AddContent(string content);
        IGPTRequestPersona SetMaxTokens(int tokens);
        IGPTRequestPersona SetTemperature(float temperature);
        IGPTRequestPersona SetModel(string model);
    }

    public interface IGPTRequestBuilder : IGPTRequestPersona
    {
        IGPTRequestBuilder FormatAsJSON();
        IGPTRequestBuilder FormatAsCSV();
        GPTRequest Build();
    }

    public class GPTRequestBuilder : IGPTRequestPersona, IGPTRequestBuilder
    {
        private static string _defaultPersona = "You are a fantasy writer like George R Martin or Tolkien";
        private List<ChatMessage> _messages = new List<ChatMessage>();
        private int? _max_tokens = null;
        private float _temperature = 0.7f;
        private string _model = "gpt-4o";

        public GPTRequestBuilder(string persona)
        {
            _messages.Add(new SystemChatMessage(persona));
        }

        public IGPTRequestPersona SetMaxTokens(int tokens)
        {   
            _max_tokens = tokens;
            return this;
        }
        public IGPTRequestPersona SetTemperature(float temperature)
        {
            _temperature = temperature;
            return this;
        }

        public IGPTRequestPersona SetModel(string model)
        {
            _model = model;
            return this;
        }

        public static GPTRequestBuilder DefaultPersona()
        {
            return new GPTRequestBuilder(_defaultPersona);
        }

        public static GPTRequestBuilder Persona(string persona)
        {
            return new GPTRequestBuilder(persona);
        }

        public IGPTRequestBuilder AddContent(string content)
        {
            _messages.Add(new UserChatMessage(content));
            return this;
        }

        public IGPTRequestBuilder FormatAsJSON()
        {
            _messages.Add(new UserChatMessage("Do not wrap the beginning and end of json in JSON markers. Only return valid JSON"));
            return this;
        }

        public IGPTRequestBuilder FormatAsCSV()
        {
            _messages.Add(new UserChatMessage("Format the answer in a comma delimited list. Respond with this list only."));
            return this;
        }

        public GPTRequest Build()
        {
            return new GPTRequest()
            {
                model = "gpt-4o",
                messages = _messages,
                //max_tokens = _max_tokens,
                temperature = _temperature
            };
        }
    }
}

namespace Telegram.Bot.Examples.Echo
{
    public static class Configuration
    {
        public readonly static string BotToken = "1133650379:AAFp_TD2eZemWCFTIlPf0niCcW1nR7G36VQ";

#if USE_PROXY
        public static class Proxy
        {
            public readonly static string Host = "{PROXY_ADDRESS}";
            public readonly static int Port = 8080;
        }
#endif
    }
}

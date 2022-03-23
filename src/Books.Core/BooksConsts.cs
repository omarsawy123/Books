using Books.Debugging;

namespace Books
{
    public class BooksConsts
    {
        public const string LocalizationSourceName = "Books";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "4b39da82f500400a8b78e7adec4c3f86";
    }
}

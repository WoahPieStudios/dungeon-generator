namespace DungeonGenerator.Tools
{
    /// <summary>
    /// Generates a random string.
    /// </summary>
    public static class RandomStringGenerator
    {
        /// <summary>
        /// Contains all of the symbols for the string generator.
        /// </summary>
        private const string Symbols = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        /// <summary>
        /// Contains only the numeric values and the lowercase letters.
        /// </summary>
        private const string SymbolsLowerNumeric = "1234567890abcdefghijklmnopqrstuvwxyz";
        /// <summary>
        /// Contains only the numeric values and the uppercase letters.
        /// </summary>
        private const string SymbolsUpperNumeric = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        /// <summary>
        /// Contains only the uppercase and lowercase letters.
        /// </summary>
        private const string SymbolsLowerUpper = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        /// <summary>
        /// Contains only the lowercase letters.
        /// </summary>
        private const string SymbolsLower = "abcdefghijklmnopqrstuvwxyz";
        /// <summary>
        /// Contains only the uppercase letters.
        /// </summary>
        private const string SymbolsUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        /// <summary>
        /// Contains only the numerical symbols.
        /// </summary>
        private const string SymbolsNumeric = "1234567890";

        /// <summary>
        /// Generates a random string that can have a specified length and format.
        /// </summary>
        /// <param name="length">The length of the string to be generated. Default length is 6.</param>
        /// <param name="stringFormat">The format of the string to be generated. Default format is all.</param>
        /// <returns>Returns a random string based on the parameters given.</returns>
        public static string GenerateString(int length = 6, StringFormat stringFormat = StringFormat.All)
        {
            var pw = string.Empty;

            var s = stringFormat switch
            {
                StringFormat.All => Symbols,
                StringFormat.Lower => SymbolsLower,
                StringFormat.Upper => SymbolsUpper,
                StringFormat.Numeric => SymbolsNumeric,
                StringFormat.LowerUpper => SymbolsLowerUpper,
                StringFormat.LowerNumeric => SymbolsLowerNumeric,
                StringFormat.UpperNumeric => SymbolsUpperNumeric,
                _ => throw new System.ArgumentOutOfRangeException(nameof(stringFormat), stringFormat, "StringFormat not found")
            };

            for (var i = 0; i < length; i++)
            {
                pw += s[UnityEngine.Random.Range(0, s.Length)];
            }

            return pw;
        }
    }

    /// <summary>
    /// A list of available formats that GenerateString returns
    /// </summary>
    public enum StringFormat
    {
        /// <summary>
        /// Uses <see cref="RandomStringGenerator.Symbols"/>
        /// </summary>
        All = 0,
        /// <summary>
        /// Uses <see cref="RandomStringGenerator.SymbolsLower"/>
        /// </summary>
        Lower = 1,
        /// <summary>
        /// Uses <see cref="RandomStringGenerator.SymbolsUpper"/>
        /// </summary>
        Upper = 2,
        /// <summary>
        /// Uses <see cref="RandomStringGenerator.SymbolsNumeric"/>
        /// </summary>
        Numeric = 3,
        /// <summary>
        /// Uses <see cref="RandomStringGenerator.SymbolsLowerUpper"/>
        /// </summary>
        LowerUpper = 4,
        /// <summary>
        /// Uses <see cref="RandomStringGenerator.SymbolsLowerNumeric"/>
        /// </summary>
        LowerNumeric = 5,
        /// <summary>
        /// Uses <see cref="RandomStringGenerator.SymbolsUpperNumeric"/>
        /// </summary>
        UpperNumeric = 6,
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;

namespace PowerEX
{
    #region Tokenizer Class XML
    /// <summary>
    /// A tokenizer used by the parser.
    /// </summary>
    #endregion
    public class Tokenizer
    {
        #region CurrentCharacter Char XML
        /// <summary>
        /// The current character that the tokenizer is on.
        /// </summary>
        #endregion
        public Char CurrentCharacter { get; private set; }

        #region Identifier String XML
        /// <summary>
        /// The current identifier string used to get variable or function identifiers.
        /// </summary>
        #endregion
        public string Identifier { get; private set; }

        #region Number Decimal XML
        /// <summary>
        /// The current number parsed by the tokenizer.
        /// </summary>
        #endregion
        public Decimal Number { get; private set; }

        #region Token Token XML
        /// <summary>
        /// The current parsed token.
        /// </summary>
        #endregion
        public Token Token { get; private set; }


        #region Reader TextReader XML
        /// <summary>
        /// The tokenizer's text reader.
        /// </summary>
        #endregion
        public TextReader Reader { get; private set; }


        #region NextToken Method XML
        /// <summary>
        /// Tells the tokenizer to parse the token at the current character.
        /// </summary>
        #endregion
        public void NextToken()
        {
            while (Char.IsWhiteSpace(CurrentCharacter))
            {
                this.NextCharacter();
            }

            switch (CurrentCharacter)
            {
                case '+':
                    this.NextCharacter();
                    Token = Token.Addition;
                    return;

                case '-':
                    this.NextCharacter();
                    Token = Token.Subtraction;
                    return;

                case '*':
                    this.NextCharacter();
                    Token = Token.Multiplication;
                    return;

                case '/':
                    this.NextCharacter();
                    Token = Token.Division;
                    return;

                case '%':
                    this.NextCharacter();
                    Token = Token.Modulus;
                    return;

                case '>':
                    this.NextCharacter();
                    Token = Token.GreaterThan;
                    return;

                case '<':
                    this.NextCharacter();
                    Token = Token.LessThan;
                    return;

                case '=':
                    this.NextCharacter();
                    Token = Token.Equal;
                    return;

                case '(':
                    this.NextCharacter();
                    Token = Token.OpeningParenthesis;
                    return;

                case ')':
                    this.NextCharacter();
                    Token = Token.ClosingParenthesis;
                    return;

                case ',':
                    this.NextCharacter();
                    Token = Token.Comma;
                    return;


                case '\0':
                    Token = Token.EOF;
                    return;
            }

            if (Char.IsDigit(CurrentCharacter) || CurrentCharacter == '.')
            {
                StringBuilder StringBuilder = new StringBuilder();
                bool HasDecimalPoint = false;
                while (Char.IsDigit(CurrentCharacter) || (!HasDecimalPoint && CurrentCharacter == '.'))
                {
                    StringBuilder.Append(CurrentCharacter);
                    HasDecimalPoint = CurrentCharacter == '.';
                    this.NextCharacter();
                }

                Number = Decimal.Parse(StringBuilder.ToString(), CultureInfo.InvariantCulture);
                Token = Token.Number;
                return;
            }

            if (Char.IsLetter(CurrentCharacter))
            {
                StringBuilder StringBuilder = new StringBuilder();

                while (Char.IsLetter(CurrentCharacter))
                {
                    StringBuilder.Append(CurrentCharacter);
                    this.NextCharacter();
                }

                Identifier = StringBuilder.ToString();
                Token = Token.Identifier;
                return;
            }

            throw new Exception($"Unexpected character: `{CurrentCharacter}`.");
        }

        #region NextCharacter Method XML
        /// <summary>
        /// Tells the tokenizer to move to the next character.
        /// </summary>
        #endregion
        public void NextCharacter()
        {
            int Character = Reader.Read();
            CurrentCharacter = ((Character >= 0) ? ((Char)Character) : '\0');
        }


        #region Tokenizer Constructor XML
        /// <summary>
        /// Creates a tokenizer instance.
        /// </summary>
        /// <param name="Reader">The tokenizer text reader.</param>
        #endregion
        public Tokenizer(TextReader Reader)
        {
            this.Reader = Reader;
            this.NextCharacter();
            this.NextToken();
        }

    }
}

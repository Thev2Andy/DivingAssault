using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerEX
{
    #region Token Enum XML
    /// <summary>
    /// A collection of tokens used in the mathematical expression evaluator.
    /// </summary>
    #endregion
    public enum Token
    {
        #region Identifier Enum Entry XML
        /// <summary>
        /// Variable / Function identifier token.
        /// </summary>
        #endregion
        Identifier,

        #region Number Enum Entry XML
        /// <summary>
        /// Number token.
        /// </summary>
        #endregion
        Number,

        #region Comma Enum Entry XML
        /// <summary>
        /// Comma token.
        /// </summary>
        #endregion
        Comma,


        #region Addition Enum Entry XML
        /// <summary>
        /// Addition operation token.
        /// </summary>
        #endregion
        Addition,

        #region Subtraction Enum Entry XML
        /// <summary>
        /// Subtraction operation token.
        /// </summary>
        #endregion
        Subtraction,

        #region Multiplication Enum Entry XML
        /// <summary>
        /// Multiplication operation token.
        /// </summary>
        #endregion
        Multiplication,

        #region Division Enum Entry XML
        /// <summary>
        /// Division operation token.
        /// </summary>
        #endregion
        Division,

        #region Modulus Enum Entry XML
        /// <summary>
        /// Modulus operation token.
        /// </summary>
        #endregion
        Modulus,

        #region GreaterThan Enum Entry XML
        /// <summary>
        /// Greater than comparison, returns `1` if true.
        /// </summary>
        #endregion
        GreaterThan,

        #region LessThan Enum Entry XML
        /// <summary>
        /// Less than comparison, returns `1` if true.
        /// </summary>
        #endregion
        LessThan,

        #region And Enum Entry XML
        /// <summary>
        /// And logical operator.
        /// </summary>
        #endregion
        And,

        #region Or Enum Entry XML
        /// <summary>
        /// Or logical operator.
        /// </summary>
        #endregion
        Or,

        #region Not Enum Entry XML
        /// <summary>
        /// Not logical operator.
        /// </summary>
        #endregion
        Not,

        #region Equal Enum Entry XML
        /// <summary>
        /// Equal comparison, returns `1` if true.
        /// </summary>
        #endregion
        Equal,


        #region OpeningParenthesis Enum Entry XML
        /// <summary>
        /// Opening parenthesis token.
        /// </summary>
        #endregion
        OpeningParenthesis,

        #region ClosingParenthesis Enum Entry XML
        /// <summary>
        /// Closing parenthesis token.
        /// </summary>
        #endregion
        ClosingParenthesis,



        #region EOF Enum Entry XML
        /// <summary>
        /// End of File token.
        /// </summary>
        #endregion
        EOF
    }
}

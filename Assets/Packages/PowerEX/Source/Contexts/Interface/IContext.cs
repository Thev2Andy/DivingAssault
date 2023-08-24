using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerEX
{
    #region IContext Interface XML
    /// <summary>
    /// A simple interface to define an evaluator context.
    /// </summary>
    #endregion
    public interface IContext
    {
        #region Call Method XML
        /// <summary>
        /// Calls a function in the expression.
        /// </summary>
        /// <param name="Identifier">The function name.</param>
        /// <param name="Arguments">The function arguments.</param>
        /// <returns>The function return value.</returns>
        #endregion
        Decimal Call(string Identifier, Decimal[] Arguments);

        #region Resolve Method XML
        /// <summary>
        /// Resolves variables in the mathematical expression.
        /// </summary>
        /// <param name="Identifier">The variable name.</param>
        /// <returns>The variable value.</returns>
        #endregion
        Decimal Resolve(string Identifier);
    }
}

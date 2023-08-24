using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerEX
{
    #region EmptyContext Class XML
    /// <summary>
    /// Defines an empty evaluator context.
    /// </summary>
    #endregion
    public class EmptyContext : IContext
    {
        #region Call Method XML
        /// <inheritdoc/>
        #endregion
        public Decimal Call(string Identifier, Decimal[] Arguments) {
            throw new Exception("Unknown function `" + Identifier + "`.");
        }

        #region Resolve Method XML
        /// <inheritdoc/>
        #endregion
        public Decimal Resolve(string Identifier) {
            throw new Exception("Unknown variable `" + Identifier + "`.");
        }
    }
}

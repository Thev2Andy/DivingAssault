using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerEX
{
    #region INode Interface XML
    /// <summary>
    /// An equation 'node', performs a single operation with other nodes.
    /// </summary>
    #endregion
    public interface INode
    {
        #region Evaluate Method XML
        /// <summary>
        /// Calculates the value of the node.
        /// </summary>
        /// <param name="Context">The evaluator context to use for variables and functions.</param>
        /// <returns>The resulting value of the node.</returns>
        #endregion
        Decimal Evaluate(IContext Context);
    }
}

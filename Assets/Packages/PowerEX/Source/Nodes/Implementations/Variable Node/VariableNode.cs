using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerEX
{
    #region VariableNode Class XML
    /// <summary>
    /// A node that represents a variable in a mathematical expression.
    /// </summary>
    #endregion
    public class VariableNode : INode
    {
        #region Identifier String XML
        /// <summary>
        /// The variable's identifier.
        /// </summary>
        #endregion
        public string Identifier { get; set; }


        #region Evaluate Method XML
        /// <inheritdoc/>
        #endregion
        public Decimal Evaluate(IContext Context) {
            return Context.Resolve(Identifier);
        }



        #region VariableNode Constructor XML
        /// <summary>
        /// Creates a new variable node.
        /// </summary>
        /// <param name="Identifier">The identifier of the variable.</param>
        #endregion
        public VariableNode(string Identifier) {
            this.Identifier = Identifier;
        }
    }
}

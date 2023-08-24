using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerEX
{
    #region NumberNode Class XML
    /// <summary>
    /// A node that represents a number.
    /// </summary>
    #endregion
    public class NumberNode : INode
    {
        #region Number Decimal XML
        /// <summary>
        /// The value of the node.
        /// </summary>
        #endregion
        public Decimal Number { get; set; }


        #region ~Identifier~ ~Type~ XML
        /// <inheritdoc/>
        #endregion
        public Decimal Evaluate(IContext Context) {
            return Number;
        }



        #region ~Identifier~ ~Type~ XML
        /// <summary>
        /// Creates a new number node.
        /// </summary>
        /// <param name="Number">The node value.</param>
        #endregion
        public NumberNode(Decimal Number) {
            this.Number = Number;
        }
    }
}

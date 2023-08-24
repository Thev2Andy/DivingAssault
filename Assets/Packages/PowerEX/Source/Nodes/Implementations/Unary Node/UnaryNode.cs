using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerEX
{
    #region UnaryNode Class XML
    /// <summary>
    /// A node that calculates a simple operation on a single number.
    /// </summary>
    #endregion
    public class UnaryNode : INode
    {
        #region Operation Func XML
        /// <summary>
        /// The node's operation.
        /// </summary>
        #endregion
        public Func<Decimal, Decimal> Operation { get; set; }

        #region RightSideNode INode XML
        /// <summary>
        /// The node on the right of the operator.
        /// </summary>
        #endregion
        public INode RightSideNode { get; set; }


        #region Evaluate Method XML
        /// <inheritdoc/>
        #endregion
        public Decimal Evaluate(IContext Context)
        {
            Decimal RightSideNodeValue = RightSideNode.Evaluate(Context);

            Decimal Result = this.Operation.Invoke(RightSideNodeValue);
            return Result;
        }


        #region UnaryNode Constructor XML
        /// <summary>
        /// Creates a new unary node.
        /// </summary>
        /// <param name="RightSideNode">The node on the right of the operator.</param>
        /// <param name="Operation">The node's operation.</param>
        #endregion
        public UnaryNode(INode RightSideNode, Func<Decimal, Decimal> Operation) {
            this.RightSideNode = RightSideNode;
            this.Operation = Operation;
        }
    }
}

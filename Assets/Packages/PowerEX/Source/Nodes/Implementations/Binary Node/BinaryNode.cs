using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerEX
{
    #region BinaryNode Class XML
    /// <summary>
    /// A node that calculates a simple operation between 2 other nodes.
    /// </summary>
    #endregion
    public class BinaryNode : INode
    {
        #region Operation Func XML
        /// <summary>
        /// The node operation.
        /// </summary>
        #endregion
        public Func<Decimal, Decimal, Decimal> Operation { get; set; }


        #region LeftSideNode INode XML
        /// <summary>
        /// The node on the left of the operator.
        /// </summary>
        #endregion
        public INode LeftSideNode { get; set; }

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
            Decimal LeftSideNodeValue = LeftSideNode.Evaluate(Context);

            Decimal Result = this.Operation.Invoke(LeftSideNodeValue, RightSideNodeValue);
            return Result;
        }


        #region BinaryNode Constructor XML
        /// <summary>
        /// Creates a new binary node.
        /// </summary>
        /// <param name="LeftSideNode">The node on the left of the operator.</param>
        /// <param name="RightSideNode">The node on the right of the operator.</param>
        /// <param name="Operation">The node operation.</param>
        #endregion
        public BinaryNode(INode LeftSideNode, INode RightSideNode, Func<Decimal, Decimal, Decimal> Operation) {
            this.LeftSideNode = LeftSideNode;
            this.RightSideNode = RightSideNode;

            this.Operation = Operation;
        }
    }
}

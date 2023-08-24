using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerEX
{
    #region FunctionNode Class XML
    /// <summary>
    /// A node that represents a function in a mathematical expression.
    /// </summary>
    #endregion
    public class FunctionNode : INode
    {
        #region Identifier String XML
        /// <summary>
        /// The function identifier.
        /// </summary>
        #endregion
        public string Identifier { get; set; }

        #region Arguments INode Array XML
        /// <summary>
        /// The arguments of the function.
        /// </summary>
        #endregion
        public INode[] Arguments { get; set; }


        #region Evaluate Method XML
        /// <inheritdoc/>
        #endregion
        public Decimal Evaluate(IContext Context)
        {
            Decimal[] ArgumentValues = new Decimal[Arguments.Length];
            for (int I = 0; I < Arguments.Length; I++)
            {
                ArgumentValues[I] = Arguments[I].Evaluate(Context);
            }

            return Context.Call(Identifier, ArgumentValues);
        }



        #region FunctionNode Constructor XML
        /// <summary>
        /// Creates a new function node.
        /// </summary>
        /// <param name="Identifier">The name / identifier of the function.</param>
        /// <param name="Arguments">The function arguments.</param>
        #endregion
        public FunctionNode(string Identifier, INode[] Arguments)
        {
            this.Identifier = Identifier;
            this.Arguments = Arguments;
        }
    }
}

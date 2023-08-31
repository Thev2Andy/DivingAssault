using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerEX
{
    #region Parser Class XML
    /// <summary>
    /// Parses and evaluates mathematical expressions.
    /// </summary>
    #endregion
    public class Parser
    {
        private Tokenizer Tokenizer { get; set; }

        #region Parse Method XML
        /// <summary>
        /// Parses and evaluates a mathematical expression.
        /// </summary>
        /// <param name="Expression">The expression to parse.</param>
        /// <param name="Context">The evaluator's function and variable context.</param>
        /// <returns>The result of the expression.</returns>
        #endregion
        public static Decimal Parse(string Expression, IContext Context)
        {
            Parser Parser = new Parser(new Tokenizer(new StringReader(Expression)));
            return Parser.ParseExpression().Evaluate(Context);
        }



        private INode ParseExpression()
        {
            INode Expression = ParseOrderOne();

            if (Tokenizer.Token != Token.EOF)
                throw new Exception("Unexpected characters at end of expression.");

            return Expression;
        }

        private INode ParseOrderOne()
        {
            INode LeftHandSide = ParseOrderTwo();

            while (true)
            {
                Func<Decimal, Decimal, Decimal> Operation = null;
                if (Tokenizer.Token == Token.Addition)
                {
                    Operation = ((A, B) => A + B);
                }

                else if (Tokenizer.Token == Token.Subtraction)
                {
                    Operation = ((A, B) => A - B);
                }

                else if (Tokenizer.Token == Token.And)
                {
                    Operation = ((A, B) => ((A != 0 && B != 0) ? 1m : 0m));
                }

                else if (Tokenizer.Token == Token.Or)
                {
                    Operation = ((A, B) => ((A != 0 || B != 0) ? 1m : 0m));
                }

                else if (Tokenizer.Token == Token.GreaterThan)
                {
                    Operation = ((A, B) => ((A > B) ? 1m : 0m));
                }

                else if (Tokenizer.Token == Token.LessThan)
                {
                    Operation = ((A, B) => ((A < B) ? 1m : 0m));
                }

                else if (Tokenizer.Token == Token.Equal)
                {
                    Operation = ((A, B) => ((A == B) ? 1m : 0m));
                }

                if (Operation == null)
                    return LeftHandSide;

                Tokenizer.NextToken();

                INode RightHandSide = ParseOrderTwo();

                LeftHandSide = new BinaryNode(LeftHandSide, RightHandSide, Operation);
            }
        }

        private INode ParseOrderTwo()
        {
            INode LeftHandSide = ParseUnary();

            while (true)
            {
                Func<Decimal, Decimal, Decimal> Operation = null;
                if (Tokenizer.Token == Token.Multiplication)
                {
                    Operation = ((A, B) => A * B);
                }

                else if (Tokenizer.Token == Token.Division)
                {
                    Operation = ((A, B) => A / B);
                }

                else if (Tokenizer.Token == Token.Modulus)
                {
                    Operation = ((A, B) => A % B);
                }

                if (Operation == null)
                    return LeftHandSide;

                Tokenizer.NextToken();

                INode RightHandSide = ParseUnary();

                LeftHandSide = new BinaryNode(LeftHandSide, RightHandSide, Operation);
            }
        }


        private INode ParseUnary()
        {
            if (Tokenizer.Token == Token.Addition)
            {
                Tokenizer.NextToken();
                return ParseUnary();
            }

            if (Tokenizer.Token == Token.Subtraction)
            {
                Tokenizer.NextToken();
                INode RightHandSide = ParseUnary();
                return new UnaryNode(RightHandSide, ((A) => -A));
            }

            if (Tokenizer.Token == Token.Not)
            {
                Tokenizer.NextToken();
                INode RightHandSide = ParseUnary();
                return new UnaryNode(RightHandSide, ((A) => ((A == 0) ? 1m : 0m)));
            }

            return ParseLeaf();
        }

        private INode ParseLeaf()
        {
            if (Tokenizer.Token == Token.Number)
            {
                NumberNode Node = new NumberNode(Tokenizer.Number);
                Tokenizer.NextToken();
                return Node;
            }

            if (Tokenizer.Token == Token.OpeningParenthesis)
            {
                Tokenizer.NextToken();

                INode Node = ParseOrderOne();

                if (Tokenizer.Token != Token.ClosingParenthesis)
                    throw new Exception("Expected character `)`.");

                Tokenizer.NextToken();
                return Node;
            }

            if (Tokenizer.Token == Token.Identifier)
            {
                string Identifier = Tokenizer.Identifier;
                Tokenizer.NextToken();

                if (Tokenizer.Token != Token.OpeningParenthesis)
                {
                    return new VariableNode(Identifier);
                }

                else
                {
                    Tokenizer.NextToken();

                    List<INode> Arguments = new List<INode>();
                    while (true)
                    {
                        Arguments.Add(ParseOrderOne());

                        if (Tokenizer.Token == Token.Comma)
                        {
                            Tokenizer.NextToken();
                            continue;
                        }

                        break;
                    }

                    if (Tokenizer.Token != Token.ClosingParenthesis)
                        throw new Exception("Expected `)` token.");

                    Tokenizer.NextToken();

                    return new FunctionNode(Identifier, Arguments.ToArray());
                }
            }

            throw new Exception($"Unexpected token: `{Tokenizer.Token}`.");
        }



        private Parser(Tokenizer Tokenizer) {
            this.Tokenizer = Tokenizer;
        }
    }
}
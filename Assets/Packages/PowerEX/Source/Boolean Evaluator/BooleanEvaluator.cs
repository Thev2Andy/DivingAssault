using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerEX
{
    #region BooleanEvaluator Class XML
    /// <summary>
    /// Parses and evaluates boolean expressions.
    /// </summary>
    #endregion
    public static class BooleanEvaluator
    {
        #region Evaluate Method XML
        /// <summary>
        /// Parses and evaluates a simple boolean expression.
        /// </summary>
        /// <param name="Expression">The expression to parse</param>
        /// <returns>The result of the expression.</returns>
        #endregion
        public static bool Evaluate(string Expression)
        {
            Stack<bool> OperandStack = new Stack<bool>();
            Stack<Char> OperatorStack = new Stack<Char>();

            Expression = Expression.Replace(" ", "");

            for (int I = 0; I < Expression.Length; I++)
            {
                Char Character = Expression[I];

                if (Character == '0' || Character == '1') {
                    OperandStack.Push(Character == '1');
                }

                else if (Character == '(') {
                    OperatorStack.Push(Character);
                }

                else if (Character == ')')
                {
                    while (OperatorStack.Count > 0 && OperatorStack.Peek() != '(') {
                        EvaluateTopOperator(OperandStack, OperatorStack);
                    }

                    if (OperatorStack.Count > 0 && OperatorStack.Peek() == '(') {
                        OperatorStack.Pop();
                    }
                }

                else if (Character == '!') {
                    OperatorStack.Push(Character);
                }

                else if (Character == '&' || Character == '|')
                {
                    while (OperatorStack.Count > 0 && OperatorStack.Peek() != '(' && OperatorStack.Peek() != '!') {
                        EvaluateTopOperator(OperandStack, OperatorStack);
                    }

                    OperatorStack.Push(Character);
                }
            }

            while (OperatorStack.Count > 0) {
                EvaluateTopOperator(OperandStack, OperatorStack);
            }

            return OperandStack.Pop();
        }

        private static void EvaluateTopOperator(Stack<bool> OperandStack, Stack<Char> OperatorStack)
        {
            Char OperatorChar = OperatorStack.Pop();
            if (OperatorChar == '!')
            {
                bool Operand = OperandStack.Pop();
                OperandStack.Push(!Operand);
            }

            else
            {
                bool Operand1 = OperandStack.Pop();
                bool Operand2 = OperandStack.Pop();
                bool Result = false;

                if (OperatorChar == '&') {
                    Result = Operand1 && Operand2;
                }

                else {
                    Result = Operand1 || Operand2;
                }

                OperandStack.Push(Result);
            }
        }

    }
}

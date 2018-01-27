using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Threading;
 
namespace xtremax_web.Libs
{
    class RPNConverter {

      // Associativity constants for operators
      private static int LEFT_ASSOC = 0;
      private static int RIGHT_ASSOC = 1;

      // Supported operators
      public static Dictionary<string, int[]> operators = new Dictionary<string, int[]>() {
        { "+",  new int[] { 0, LEFT_ASSOC } },
        { "-", new int[] { 0, LEFT_ASSOC } },
        { "*", new int[] { 5, LEFT_ASSOC } },
        { "/", new int[] { 5, LEFT_ASSOC } },
        { "%", new int[] { 5, LEFT_ASSOC } },
        { "^", new int[] { 10, RIGHT_ASSOC } }
      };

      /**
       * Test if a certain is an operator .
       * @param token The token to be tested .
       * @return True if token is an operator . Otherwise False .
       */
      private static bool isOperator(String token) {
        return operators.ContainsKey(token);
      }

      /**
       * Test the associativity of a certain operator token .
       * @param token The token to be tested (needs to operator).
       * @param type LEFT_ASSOC or RIGHT_ASSOC
       * @return True if the tokenType equals the input parameter type .
       */
      private static bool isAssociative(string token, int type) {
        if (!isOperator(token)) {
          throw new ArgumentException("Invalid token: " + token);
        }
        if (operators[token][1] == type) {
          return true;
        }
        return false;
      }

      /**
       * Compare precendece of two operators.
       * @param token1 The first operator .
       * @param token2 The second operator .
       * @return A negative number if token1 has a smaller precedence than token2,
       * 0 if the precendences of the two tokens are equal, a positive number
       * otherwise.
       */
      private static int cmpPrecedence(string token1, string token2) {
        if (!isOperator(token1) || !isOperator(token2)) {
          throw new ArgumentException("Invalied tokens: " + token1
              + " " + token2);
        }
        return operators[token1][0] - operators[token2][0];
      }

      /**
      * Check if stack is not empty
      * @param stack The reference of stack
      * @return boo
      */
      public static bool isNotEmpty(ref Stack<string> stack) {
        return stack.Count != 0;
      }

      /**
      * Convert 
      * @param string[] array of strings that token inputted
      * @return string RPN forms of token inputted
      */
      public static string infixToRPN(string[] inputTokens) {
        List<string> output = new List<string>();
        Stack<string> stack = new Stack<string>();
        // For all the input tokens [S1] read the next token [S2]
        foreach(string token in inputTokens) {
          if (isOperator(token)) {
            // If token is an operator (x) [S3]
            while (isNotEmpty(ref stack) && isOperator(stack.Peek())) {
              // [S4]
              if ((isAssociative(token, LEFT_ASSOC) && cmpPrecedence(
                  token, stack.Peek()) <= 0)
                  || (isAssociative(token, RIGHT_ASSOC) && cmpPrecedence(
                      token, stack.Peek()) < 0)) {
                output.Add(stack.Pop()); 	// [S5] [S6]
              }
              break;
            }
            // Push the new operator on the stack [S7]
            stack.Push(token);
          } else if (token.Equals("(")) {
            stack.Push(token); 	// [S8]
          } else if (token.Equals(")")) {
            // [S9]
            while (isNotEmpty(ref stack) && !stack.Peek().Equals("(")) {
              output.Add(stack.Pop()); // [S10]
            }
            stack.Pop(); // [S11]
          } else {
            Console.WriteLine(token);
            output.Add(token); // [S12]
          }
        }
        while (isNotEmpty(ref stack)) {
          output.Add(stack.Pop()); // [S13]
        }
        return string.Join(" ", output.ToArray());
      }

    }
}
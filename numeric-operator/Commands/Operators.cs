namespace NumericOperator
{
    public delegate decimal Operator(decimal leftOperand, decimal rightOperand);

    public partial class Operators
    {
        public static decimal Add(decimal leftOperand, decimal rightOperand)
        {
            return leftOperand + rightOperand;
        }

        public static decimal Subtract(decimal leftOperand, decimal rightOperand)
        {
            return leftOperand - rightOperand;
        }

        public static decimal Divide(decimal leftOperand, decimal rightOperand)
        {
            return leftOperand / rightOperand;
        }

        public static decimal Multiply(decimal leftOperand, decimal rightOperand)
        {
            return leftOperand * rightOperand;
        }
    }
}

namespace DDD.Domain.TestLib;

public class Class1
{
    public static int Add(int a, int b)
    {

        if (a < 0 || b < 0)
        {
            throw new InputException("マイナスの値は入力できません");
        }
        return a + b;
    }
}
